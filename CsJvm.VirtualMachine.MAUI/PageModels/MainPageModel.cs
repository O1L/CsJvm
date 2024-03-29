﻿using CsJvm.Abstractions.Disasm;
using CsJvm.Abstractions.VirtualMachine;
using CsJvm.VirtualMachine.MAUI.Models;
using FreshMvvm.Maui;
using System.Windows.Input;
using Frame = CsJvm.Models.Frame;

namespace CsJvm.VirtualMachine.MAUI.PageModels
{
    public class MainPageModel : FreshBasePageModel
    {
        public string Title => "Main title";

        private readonly IDisasm _disasm;
        private readonly IJavaMachine _machine;

        private Frame _currentFrame;

        public DisasmModel Disasm { get; set; }


        public int SelectedIndex { get; private set; }

        public MainPageModel(IDisasm disasm, IJavaMachine machine)
        {
            _disasm = disasm;
            _machine = machine;

            Task.Run(Load);
        }

        private async Task Load()
        {
            var success = await _machine.LoadAsync("D:/dev/JVMTest/JVMTest/out/artifacts/JVMTest_jar/JVMTest.jar");
            if (!success)
                return;

            _currentFrame = _machine.MainThread.CurrentMethod;
            var asm = _disasm.GetOpcodes(_machine.MainThread.CurrentMethod.Code, _machine.MainThread.CurrentMethod.CpInfo);

            Disasm = new()
            {
                CurrentPc = _machine.MainThread.ProgramCounter,
                BreakPoint = 0,
                Opcodes = asm,
                MethodName = _machine.MainThread.CurrentMethod.MethodName,
                FramesCount = _machine.MainThread.FrameSize,

                OperandStack = _machine.MainThread.CurrentMethod.OperandStack.Select(x => new VariableModel
                {
                    Index = 0,
                    TypeName = x.GetType().Name,
                    Value = x.ToString()
                }).ToArray(),

                LocalVariables = _machine.MainThread.CurrentMethod.LocalVariables.Select((x, i) => new VariableModel
                {
                    Index = i,
                    TypeName = "unspecified",
                    Value = "null"
                }).ToArray()
            };

            //UpdateView();
        }

        public ICommand StepIntoCommand => new Command(async () =>
        {
            await _machine.StepIntoAsync();
            UpdateView();
        });

        public ICommand StepOverCommand => new Command(async () =>
        {
            await _machine.StepOverAsync();
            UpdateView();
        });

        private void UpdateView()
        {
            // check if the method has changed
            if (_currentFrame != _machine.MainThread.CurrentMethod)
            {
                Disasm.PreviousMethodName = _currentFrame.MethodName;
                _currentFrame = _machine.MainThread.CurrentMethod;
                Disasm.MethodName = _currentFrame.MethodName;
                Disasm.Opcodes = _disasm.GetOpcodes(_machine.MainThread.CurrentMethod.Code, _machine.MainThread.CurrentMethod.CpInfo);
            }

            Disasm.CurrentPc = _machine.MainThread.ProgramCounter;
            Disasm.FramesCount = _machine.MainThread.FrameSize;

            // fill opernd stack
            Disasm.OperandStack = _machine.MainThread.CurrentMethod.OperandStack.Select((x, i) => new VariableModel
            {
                Index = i,
                TypeName = x != null ? x.GetType().Name : "unspecified",
                Value = x != null ? x.ToString() : "null"
            }).ToArray();

            // fill local variables
            Disasm.LocalVariables = _machine.MainThread.CurrentMethod.LocalVariables.Select((x, i) => new VariableModel
            {
                Index = i,
                TypeName = x != null ? x.GetType().Name : "unspecified",
                Value = x != null ? x.ToString() : "null"
            }).ToArray();

            OnPropertyChanged(nameof(Disasm));
        }
    }
}

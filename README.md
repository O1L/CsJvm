# CsJvm
A Java Virtual Machine implementation in pure .NET (work-in-progress)

Implemented most of opcodes, interpreter only yet. Program has a simple UI based on MAUI (still is very WIP):
![image](https://user-images.githubusercontent.com/5551509/223751709-3175f689-c7d3-4fa6-87ea-4d30b0381919.png)

Project includes:
 - Abstractions layer
 - Java bytecode disassembler (similar to javap)
 - JAR and .class files loader
 - JVM-specific models layer
 - Java Virtual Machine implementation
 - CLI project to test and run simple applications
 - MAUI project to research how your Java application works in the low level
 
You are able to execute opcodes one by one and inspect the opcodes stack and local variables on each step. Do not forget to specify the JRE path in appsettings.json (for example, C:\Users\your.name\.jdks\corretto-1.8.0_332\jre\lib). The path must contain Java Runtime library (rt.jar), so, you can use Java version up to 1.8.

Known issues:
 - Multithreading support not implemented yet
 - Some opcodes not implemented yet and need more accurately revise and implementation (especially, reference ops)
 - The base object class used to store values in the operand stack and local values, so, hello frequent boxing-unboxing (needs to be redone)
 
Why am I doing this? Just for fun. I am an enthusiast of hardware emulation and virtual machines.

Special thanks to neoexpert's test application. It is really helpful: https://gitlab.com/neoexpert/jvm/-/blob/master/JVMTest.java

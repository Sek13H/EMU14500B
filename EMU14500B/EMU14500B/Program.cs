using System;

class EMU14500B
{
    private byte accumulator = 0; // Accumulator
    private byte programCounter = 0; // Program Counter
    private byte[] memory = new byte[16]; // Memory

    // Flags
    private bool carryFlag = false;
    private bool zeroFlag = false;

    // Instructions
    private enum Instruction
    {
        NOP = 0x00, // No Operation
        LDA = 0x01, // Load Accumulator
        AND = 0x02, // AND with Accumulator
        ORA = 0x03, // OR with Accumulator
        XOR = 0x04, // XOR with Accumulator
        INVERT = 0x05, // Invert Accumulator
        JMP = 0x06, // Jump to address
        JZ = 0x07, // Jump if zero
        JC = 0x08, // Jump if carry
        RLC = 0x09, // Rotate left Accumulator
        RRC = 0x0A, // Rotate right Accumulator
        STORE = 0x0B, // Store Accumulator to memory
        MOVE = 0x0C, // Move value from memory to Accumulator
        HALT = 0x0F, // Halt execution
        DEC = 0x0D, // Decrement Accumulator
        INC = 0x0E // Increment Accumulator
    }

    public void Run()
    {
        while (true)
        {
            Console.Clear();
            PrintState();
            Console.WriteLine("EMU14500B!");
            Console.WriteLine("By Reimolaev");
            Console.WriteLine("Choose input method:");
            Console.WriteLine("1. Enter instructions manually");
            Console.WriteLine("2. Enter assembly code (ASM)");
            Console.WriteLine("Enter 'exit' to quit");
            string choice = Console.ReadLine();

            if (choice.ToLower() == "exit")
                break;

            if (choice == "1")
            {
                Console.WriteLine("Enter instructions (e.g., LDA 0x01), or 'HALT' to stop:");
                ManualInput();
            }
            else if (choice == "2")
            {
                Console.WriteLine("Enter assembly code (e.g., LDA 0x01; AND 0x02):");
                string asmCode = Console.ReadLine();
                ExecuteAssembly(asmCode);   
            }
            else
            {
                Console.WriteLine("Invalid choice!");
            }
        }
    }

    private void ManualInput()
    {
        string input;
        while (true)
        {
            input = Console.ReadLine();
            if (input.ToUpper() == "HALT")
            {
                Console.WriteLine("Execution halted!");
                break;
            }

            var parts = input.Split(' ');
            if (parts.Length > 0)
            {
                string command = parts[0].ToUpper();
                string operand = parts.Length > 1 ? parts[1] : null;

                switch (command)
                {
                    case "LDA":
                        LoadAccumulator(operand);
                        break;
                    case "AND":
                        AndAccumulator(operand);
                        break;
                    case "OR":
                        OrAccumulator(operand);
                        break;
                    case "XOR":
                        XorAccumulator(operand);
                        break;
                    case "INVERT":
                        InvertAccumulator();
                        break;
                    case "STORE":
                        StoreAccumulator(operand);
                        break;
                    case "MOVE":
                        MoveToAccumulator(operand);
                        break;
                    case "JMP":
                        JumpToAddress(operand);
                        break;
                    case "JZ":
                        JumpIfZero(operand);
                        break;
                    case "JC":
                        JumpIfCarry(operand);
                        break;
                    case "RLC":
                        RotateLeft();
                        break;
                    case "RRC":
                        RotateRight();
                        break;
                    case "DEC":
                        DecrementAccumulator();
                        break;
                    case "INC":
                        IncrementAccumulator();
                        break;
                    case "NOP":
                        Nop();
                        break;
                    case "HALT":
                        Halt();
                        return;
                    default:
                        Console.WriteLine($"Unknown instruction: {command}");
                        break;

                }
            }
        }
    }

    private void ExecuteAssembly(string asmCode)
    {
        string[] lines = asmCode.Split(new[] { ';', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            var parts = line.Split(' ');
            if (parts.Length > 0)
            {
                string command = parts[0].ToUpper();    
                string operand = parts.Length > 1 ? parts[1] : null;

                switch (command)
                {
                    case "LDA":
                        LoadAccumulator(operand);
                        break;
                    case "AND":
                        AndAccumulator(operand);
                        break;
                    case "OR":
                        OrAccumulator(operand);
                        break;
                    case "XOR":
                        XorAccumulator(operand);
                        break;
                    case "INVERT":
                        InvertAccumulator();
                        break;
                    case "STORE":
                        StoreAccumulator(operand);
                        break;
                    case "MOVE":
                        MoveToAccumulator(operand);
                        break;
                    case "JMP":
                        JumpToAddress(operand);
                        break;
                    case "JZ":
                        JumpIfZero(operand);
                        break;
                    case "JC":
                        JumpIfCarry(operand);
                        break;
                    case "RLC":
                        RotateLeft();
                        break;
                    case "RRC":
                        RotateRight();
                        break;
                    case "DEC":
                        DecrementAccumulator();
                        break;
                    case "INC":
                        IncrementAccumulator();
                        break;
                    case "NOP":
                        Nop();
                        break;
                    case "HALT":
                        Halt();
                        return;
                    default:
                        Console.WriteLine($"Unknown instruction: {command}");
                        break;
                }
            }
        }
    }

    private void Nop()
    {
        programCounter++;
    }

    private void LoadAccumulator(string address)
    {
        byte addr = Convert.ToByte(address, 16);
        accumulator = memory[addr];
    }

    private void AndAccumulator(string address)
    {
        byte addr = Convert.ToByte(address, 16);
        accumulator &= memory[addr];
    }

    private void OrAccumulator(string address)
    {
        byte addr = Convert.ToByte(address, 16);
        accumulator |= memory[addr];
    }

    private void XorAccumulator(string address)
    {
        byte addr = Convert.ToByte(address, 16);
        accumulator ^= memory[addr];
    }

    private void InvertAccumulator()
    {
        accumulator = (byte)(~accumulator);
    }

    private void StoreAccumulator(string address)
    {
        byte addr = Convert.ToByte(address, 16);
        memory[addr] = accumulator;
    }

    private void MoveToAccumulator(string address)
    {
        byte addr = Convert.ToByte(address, 16);
        accumulator = memory[addr];
    }

    private void JumpToAddress(string address)
    {
        programCounter = Convert.ToByte(address, 16);
    }

    private void JumpIfZero(string address)
    {
        if (zeroFlag)
            programCounter = Convert.ToByte(address, 16);
        else
            programCounter++;
    }

    private void JumpIfCarry(string address)
    {
        if (carryFlag)
            programCounter = Convert.ToByte(address, 16);
        else
            programCounter++;
    }

    private void RotateLeft()
    {
        carryFlag = (accumulator & 0x80) != 0;
        accumulator = (byte)((accumulator << 1) | (accumulator >> 7));
    }

    private void RotateRight()
    {
        carryFlag = (accumulator & 0x01) != 0;
        accumulator = (byte)((accumulator >> 1) | (accumulator << 7));
    }

    private void DecrementAccumulator()
    {
        accumulator--;
    }

    private void IncrementAccumulator()
    {
        accumulator++;
    }

    private void Halt()
    {
        Console.WriteLine("Execution halted!");
    }

    private void PrintState()
    {
        Console.SetCursorPosition(0, 0);
        Console.WriteLine($"PC: {programCounter}, ACC: {accumulator:X2}, ZF: {zeroFlag}, CF: {carryFlag}");
    }

    public static void Main()
    {
        EMU14500B emu = new EMU14500B();
        emu.Run();
    }
}
using System;
using System.Text;
using System.Collections.Generic;

public class OldPhone
{
    // Define a mapping for the old phone pad
    private static readonly Dictionary<char, string> keypad = new Dictionary<char, string>
    {
        {'2', "ABC"}, {'3', "DEF"}, {'4', "GHI"}, {'5', "JKL"},
        {'6', "MNO"}, {'7', "PQRS"}, {'8', "TUV"}, {'9', "WXYZ"},
        {'0', " "}
    };

    // The method to process the input string according to old phone keypad rules
    public static string OldPhonePad(string input)
    {
        StringBuilder result = new StringBuilder();
        char lastKey = '\0';
        int pressCount = 0;

        foreach (char c in input)
        {
            if (c == '#')
            {
                // End of input, so break the loop
                break;
            }
            else if (c == '*')
            {
                // Backspace: Remove last character and reset current key and press count
                if (result.Length > 0)
                {
                    result.Length--; // Remove the last character
                }
                // Reset last key and press count because * cancels the last entry
                lastKey = '\0';
                pressCount = 0;
            }
            else if (c == ' ')
            {
                // Space: This indicates a pause between inputs
                if (lastKey != '\0' && pressCount > 0)
                {
                    string letters = keypad[lastKey];
                    result.Append(letters[(pressCount - 1) % letters.Length]);
                }
                lastKey = '\0'; // Reset the last key
                pressCount = 0;
            }
            else if (keypad.ContainsKey(c))
            {
                if (c == lastKey)
                {
                    pressCount++;
                }
                else
                {
                    // Process the previous key press if a new key is pressed
                    if (lastKey != '\0' && pressCount > 0)
                    {
                        string letters = keypad[lastKey];
                        result.Append(letters[(pressCount - 1) % letters.Length]);
                    }
                    lastKey = c;
                    pressCount = 1;
                }
            }
        }

        // Add the last character if there's any unprocessed key and no backspace
        if (lastKey != '\0' && pressCount > 0)
        {
            string letters = keypad[lastKey];
            result.Append(letters[(pressCount - 1) % letters.Length]);
        }

        return result.ToString();
    }

    // The Main method to run the program
    public static void Main(string[] args)
    {
        // Test cases
        Console.WriteLine(OldPhonePad("33#"));               // Output: E
        Console.WriteLine(OldPhonePad("22#"));             // Output: B
        Console.WriteLine(OldPhonePad("4433555 555666#"));   // Output: HELLO
        Console.WriteLine(OldPhonePad("8 88777444666*664#")); // Output: TURNS
    }
}


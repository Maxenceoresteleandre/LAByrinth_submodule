using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
public class DebugLogWriter : System.IO.TextWriter
{
    private StringBuilder logBuffer = new StringBuilder();

    public override void Write(char value)
    {
        base.Write(value);
        logBuffer.Append(value);

        // Check if the logging is completed, for example, when a newline character is encountered
        if (value == '\n')
        {
            // Print the accumulated log to the debug log
            Debug.Log(logBuffer.ToString());

            // Clear the buffer for the next log entry
            logBuffer.Clear();
        }
    }

    public override System.Text.Encoding Encoding
    {
        get { return System.Text.Encoding.UTF8; }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace RandomValueConsoleBot
{
    public class CommandConstants
    {
        /// <summary>
        /// Return random value
        /// </summary>
        public const String VALUE = "/value";
        /// <summary>
        /// List of all commands
        /// </summary>
        public const String START_COMMAND = "/start";
        /// <summary>
        /// List of all commands
        /// </summary>
        public const String HELP_COMMAND = "/help";


        public const String START_MESSAGE = @"Hello! This is RandomValueBot! It gets you random value any time!
                                              To get random value, type"+@"/value";
    }
}

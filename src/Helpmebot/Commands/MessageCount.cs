﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageCount.cs" company="Helpmebot Development Team">
//   Helpmebot is free software: you can redistribute it and/or modify
//   it under the terms of the GNU General Public License as published by
//   the Free Software Foundation, either version 3 of the License, or
//   (at your option) any later version.
//   
//   Helpmebot is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//   GNU General Public License for more details.
//   
//   You should have received a copy of the GNU General Public License
//   along with Helpmebot.  If not, see http://www.gnu.org/licenses/ .
// </copyright>
// <summary>
//   Returns the number of messages that have been sent by the bot to IRC
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace helpmebot6.Commands
{
    using Helpmebot;

    /// <summary>
    ///   Returns the number of messages that have been sent by the bot to IRC
    /// </summary>
    internal class Messagecount : GenericCommand
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Messagecount"/> class.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="channel">
        /// The channel.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        public Messagecount(User source, string channel, string[] args)
            : base(source, channel, args)
        {
        }

        /// <summary>
        /// The execute command.
        /// </summary>
        /// <returns>
        /// The <see cref="CommandResponseHandler"/>.
        /// </returns>
        protected override CommandResponseHandler ExecuteCommand()
        {
            string[] messageParameters = { Helpmebot6.irc.messageCount.ToString() };
            return
                new CommandResponseHandler(new Message().get("messageCountReport", messageParameters));
        }
    }
}
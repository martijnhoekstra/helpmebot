﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Rawctcp.cs" company="Helpmebot Development Team">
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
//   Sends a raw client-to-client protocol command
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace helpmebot6.Commands
{
    using Helpmebot;

    /// <summary>
    /// Sends a raw client-to-client protocol command
    /// </summary>
    internal class Rawctcp : GenericCommand
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Rawctcp"/> class.
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
        public Rawctcp(User source, string channel, string[] args)
            : base(source, channel, args)
        {
        }

        /// <summary>
        /// Actual command logic
        /// </summary>
        /// <returns>the response</returns>
        protected override CommandResponseHandler ExecuteCommand()
        {
            var args = this.Arguments;

            string cmd = GlobalFunctions.popFromFront(ref args);
            string dst = GlobalFunctions.popFromFront(ref args);

            Helpmebot6.irc.ircPrivmsg(dst, IAL.wrapCTCP(cmd, string.Join(" ", args)));

            return null;
        }
    }
}
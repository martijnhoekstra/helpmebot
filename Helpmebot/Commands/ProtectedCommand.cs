﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProtectedCommand.cs" company="Helpmebot Development Team">
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
//   Defines the ProtectedCommand type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace helpmebot6.Commands
{
    using System;

    abstract class ProtectedCommand : GenericCommand
    {
        protected override CommandResponseHandler ReallyRunCommand(User source, string channel, string[] args)
        {

            if(!AccessLog.instance().save(new AccessLog.AccessLogEntry(source, GetType(), true,channel, args)))
            {
                CommandResponseHandler errorResponse = new CommandResponseHandler();
                errorResponse.respond("Error adding to access log - command aborted.", CommandResponseDestination.ChannelDebug);
                errorResponse.respond(new Message().get("AccessDeniedAccessListFailure"), CommandResponseDestination.Default);
                return errorResponse;
            }

            this.LogMessage("Starting command execution...");
            CommandResponseHandler crh;

            try
            {
                crh = GlobalFunctions.isInArray("@confirm", args) != -1 ? this.ExecuteCommand(source, channel, args) : notConfirmed(source, channel, args);
            }
            catch (Exception ex)
            {
                Logger.instance().addToLog(ex.ToString(), Logger.LogTypes.Error);
                crh = new CommandResponseHandler(ex.Message);
            }
            this.LogMessage("Command execution complete.");
            return crh;
        }

        protected abstract CommandResponseHandler notConfirmed(User source, string channel, string[] args);
    }
}

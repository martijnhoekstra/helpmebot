﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Pie.cs" company="Helpmebot Development Team">
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
//   Defines the Pie type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace helpmebot6.Commands
{
    class Pie : FunStuff.FunCommand
    {
        protected override CommandResponseHandler ExecuteCommand(User source, string channel, string[] args)
        {
            string name = string.Join(" ", args);

            string[] messageparams = { name };
            string message = new Message().get("CmdPie", messageparams);

            return new CommandResponseHandler(message);
        }
    }
}

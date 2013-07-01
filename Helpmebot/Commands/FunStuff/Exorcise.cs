﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace helpmebot6.Commands
{
    class Exorcise : FunStuff.FunCommand
    {
        protected override CommandResponseHandler execute(User source, string channel, string[] args)
        {
            string name = string.Join(" ", args);

            string[] messageparams = { name };
            string message = new Message().get("CmdExorcise", messageparams);

            return new CommandResponseHandler(message);
        }
    }
}
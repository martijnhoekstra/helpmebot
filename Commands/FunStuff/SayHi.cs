﻿using System;
using System.Collections.Generic;
using System.Text;
using helpmebot6;

namespace helpmebot6.Commands
{
    /// <summary>
    /// Says hi to a user
    /// </summary>
    class Sayhi : GenericCommand
    {
        public Sayhi( )
        {

        }

        protected override CommandResponseHandler execute( User toUser ,string channel,  string[ ] args )
        {
            Logger.Instance( ).addToLog( "Method:" + System.Reflection.MethodInfo.GetCurrentMethod( ).DeclaringType.Name + System.Reflection.MethodInfo.GetCurrentMethod( ).Name, Logger.LogTypes.DNWB );

            string[ ] commandParams = { toUser.Nickname };
            return new CommandResponseHandler( Configuration.Singleton( ).GetMessage( "cmdSayHi1" , commandParams ) );
        }
    }
}

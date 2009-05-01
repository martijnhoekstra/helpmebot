﻿/****************************************************************************
 *   This file is part of Helpmebot.                                        *
 *                                                                          *
 *   Helpmebot is free software: you can redistribute it and/or modify      *
 *   it under the terms of the GNU General Public License as published by   *
 *   the Free Software Foundation, either version 3 of the License, or      *
 *   (at your option) any later version.                                    *
 *                                                                          *
 *   Helpmebot is distributed in the hope that it will be useful,           *
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of         *
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the          *
 *   GNU General Public License for more details.                           *
 *                                                                          *
 *   You should have received a copy of the GNU General Public License      *
 *   along with Helpmebot.  If not, see <http://www.gnu.org/licenses/>.     *
 ****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace helpmebot6
{
    public class Helpmebot6
    {
       static IAL irc ;
       static DAL dbal;
       static Configuration config;
       static CommandParser cmd;

       static string Trigger;

       public static string debugChannel;
       public static string mainChannel;

       static void Main( string[ ] args )
       {

           string server, username, password, schema;
           uint port = 0;
           server = username = password = schema = "";

           Configuration.readHmbotConfigFile( ".hmbot", ref server, ref username, ref password, ref port, ref schema );

           DAL.singleton = new DAL( server, port, username, password, schema );
           dbal = DAL.singleton;
           dbal.Connect( );

           Configuration.singleton = new Configuration( );
           config = Configuration.singleton;

           string Nickname, Username, Realname, Password, Server;
           uint Port;
           bool Wallops, Invisible;
           Nickname = config.retrieveStringOption( "ircNickname" );
           Username = config.retrieveStringOption( "ircUsername" );
           Realname = config.retrieveStringOption( "ircRealname" );
           Password = config.retrieveStringOption( "ircPassword" );
           Port = config.retrieveUintOption( "ircServerPort" );
           Server = config.retrieveStringOption( "ircServerHost" );
           Wallops = false;
           Invisible = false;

           Trigger = config.retrieveStringOption( "commandTrigger" );

           IAL.singleton = new IAL( Nickname, Username, Realname, Password, Server, Port, Wallops, Invisible );
           irc = IAL.singleton;

           CommandParser.singleton = new CommandParser( );
           cmd = CommandParser.singleton;

           irc.ConnectionRegistrationSucceededEvent += new IAL.ConnectionRegistrationEventHandler( JoinChannels );
           irc.PrivmsgEvent += new IAL.PrivmsgEventHandler( RecievedMessage );
           irc.Connect( );
       }

        static void RecievedMessage( User source, string destination, string message )
        {
            // Bot AI
            string[] helloWords = { "hi", "hey", "heya", "morning", "afternoon", "evening", "hello" };
            if ( GlobalFunctions.isInArray( message.Split( ' ' )[ 0 ].ToLower( ), helloWords ) && message.Split( ' ' )[ 1 ].ToLower( ) == irc.IrcNickname.ToLower( ) )
            {
                cmd.CommandParser_CommandRecievedEvent( source, destination, "sayhi", null );
            }
            else
            {
                string[ ] splitMessage = message.Split( ' ' );
                if ( splitMessage[ 0 ] == Trigger + irc.IrcNickname )
                {
                    GlobalFunctions.popFromFront( ref splitMessage );
                }

                string command = GlobalFunctions.popFromFront( ref splitMessage ).Substring(1);
                if ( command.Substring( 0, 1 ) == Trigger )
                {
                    cmd.CommandParser_CommandRecievedEvent( source, destination, command, splitMessage );
                }
            }
        }

        static void JoinChannels( )
        {
            debugChannel = config.retrieveStringOption( "channelDebug" );
            mainChannel = config.retrieveStringOption( "channelMain" );

            irc.IrcJoin( debugChannel );
            irc.IrcJoin( mainChannel );
        }

        

    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace helpmebot6.Commands
{
    class Access : GenericCommand
    {
        protected override CommandResponseHandler accessDenied( User source, string channel, string[ ] args )
        {
            Logger.Instance( ).addToLog( "Method:" + System.Reflection.MethodInfo.GetCurrentMethod( ).DeclaringType.Name + System.Reflection.MethodInfo.GetCurrentMethod( ).Name, Logger.LogTypes.DNWB );

            CommandResponseHandler crh = new Myaccess( ).run( source, channel, args );
            crh.append( base.accessDenied( source, channel, args ) );
            return crh;
        }

        protected override CommandResponseHandler execute( User source , string channel , string[ ] args )
        {
            Logger.Instance( ).addToLog( "Method:" + System.Reflection.MethodInfo.GetCurrentMethod( ).DeclaringType.Name + System.Reflection.MethodInfo.GetCurrentMethod( ).Name, Logger.LogTypes.DNWB );

            CommandResponseHandler crh=new CommandResponseHandler();
            if( args.Length > 1 )
            {
                switch( args[0].ToLower() )
                {
                    case "add":
                        if( args.Length > 2 )
                        {
                            User.userRights aL = User.userRights.Normal;

                            switch( args[ 2 ].ToLower( ) )
                            {
                                case "developer":
                                    aL = User.userRights.Developer;
                                    break;
                                case "superuser":
                                    aL = User.userRights.Superuser;
                                    break;
                                case "advanced":
                                    aL = User.userRights.Advanced;
                                    break;
                                case "semi-ignored":
                                    aL = User.userRights.Semiignored;
                                    break;
                                case "ignored":
                                    aL = User.userRights.Ignored;
                                    break;
                                case "normal":
                                    aL = User.userRights.Normal;
                                    break;
                                default:
                                    break;
                            }

                            crh = addAccessEntry( User.newFromString( args[ 1 ] ) , aL );
                        }
                        break;
                    case "del":
                        crh = delAccessEntry( int.Parse( args[ 1 ] ) );
                        break;
                }
                // add <source> <level>

                // del <id>
            }
            return crh;
        }

        CommandResponseHandler addAccessEntry( User newEntry , User.userRights AccessLevel )
        {
            Logger.Instance( ).addToLog( "Method:" + System.Reflection.MethodInfo.GetCurrentMethod( ).DeclaringType.Name + System.Reflection.MethodInfo.GetCurrentMethod( ).Name, Logger.LogTypes.DNWB );

            string[ ] messageParams = { newEntry.ToString( ), AccessLevel.ToString( ) };
            string message = Configuration.Singleton( ).GetMessage( "addAccessEntry", messageParams );
            
            // "Adding access entry for " + newEntry.ToString( ) + " at level " + AccessLevel.ToString( )"
            Logger.Instance( ).addToLog( "Adding access entry for " + newEntry.ToString( ) + " at level " + AccessLevel.ToString( ) , Logger.LogTypes.COMMAND );
            DAL.Singleton( ).Insert( "user", "", newEntry.Nickname, newEntry.Username, newEntry.Hostname, AccessLevel.ToString( ) );

            return new CommandResponseHandler( message );
        }

        CommandResponseHandler delAccessEntry( int id )
        {
            Logger.Instance( ).addToLog( "Method:" + System.Reflection.MethodInfo.GetCurrentMethod( ).DeclaringType.Name + System.Reflection.MethodInfo.GetCurrentMethod( ).Name, Logger.LogTypes.DNWB );

            string[ ] messageParams = { id.ToString( ) };
            string message = Configuration.Singleton( ).GetMessage( "removeAccessEntry", messageParams );

            Logger.Instance( ).addToLog( "Removing access entry #" + id.ToString( ) , Logger.LogTypes.COMMAND );
            DAL.Singleton( ).Delete( "user", 1, new DAL.WhereConds( "user_id", id.ToString( ) ) );

            return new CommandResponseHandler( message );
        }
    }
}

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
    public class User
    {
        private DAL db;

        private string _nickname, _username, _hostname;
        private userRights _accessLevel;
        private bool _retrieved_accessLevel = false;

        public User()
        {
            db = DAL.singleton;
        }

        public string Nickname
        {
            get
            {
                return _nickname;
            }
            set
            {
                _nickname = value;
            }
        }

        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username=value;
            }
        }

        public string Hostname
        {
            get
            {
                return _hostname;
            }
            set
            {
                _hostname = value;
            }
        }

        public static User newFromString(string source)
        {    
            string nick, user, host;
            nick = user = host = null;
            try
            {

                if ( ( source.Contains( "@" ) ) && ( source.Contains( "!" ) ) )
                {
                    char[ ] splitSeparators = { '!', '@' };
                    string[ ] sourceSegment = source.Split( splitSeparators, 3 );
                    nick = sourceSegment[ 0 ];
                    user = sourceSegment[ 1 ];
                    host = sourceSegment[ 2 ];
                }
                else if ( source.Contains( "@" ) )
                {
                    char[ ] splitSeparators = { '@' };
                    string[ ] sourceSegment = source.Split( splitSeparators, 2 );
                    nick = sourceSegment[ 0 ];
                    user = null;
                    host = sourceSegment[ 1 ];
                }
                else
                {
                    nick = source;
                    user = null;
                    host = null;

                }
            }
            catch ( IndexOutOfRangeException ex )
            {
                GlobalFunctions.ErrorLog( ex , System.Reflection.MethodInfo.GetCurrentMethod());
            }

            User ret = new User( );
            ret.Hostname = host;
            ret.Nickname = nick;
            ret.Username = user;

            return ret;

        }

        /// <summary>
        /// Recompiles the source string
        /// </summary>
        /// <returns>nick!user@host, OR nick@host, OR nick</returns>
        public override string ToString( )
        {
            string endResult = this.Nickname;

            if ( this.Username != null )
            {
                endResult += "!" + this.Username;
            }
            if ( this.Hostname != null )
            {
                endResult += "@" + this.Hostname;
            }

            return endResult;
        }

        public userRights AccessLevel
        {
            get
            {
                try
                {
                    if ( _retrieved_accessLevel == false )
                    {
                        string qry = "SELECT u.`user_accesslevel` FROM `user` u WHERE '" + _nickname + "' LIKE u.`user_nickname` AND '" + _username + "' LIKE u.`user_username` AND '" + _hostname + "' LIKE u.`user_hostname` ORDER BY u.`user_accesslevel` LIMIT 1;";

                        string accesslevel = db.ExecuteScalarQuery( qry ).ToString( );
                        if ( accesslevel == null )
                        {
                            accesslevel = "Normal";
                        }

                        userRights ret;

                        switch ( accesslevel )
                        {
                            case "Superuser":
                                ret = userRights.Superuser;
                                break;
                            case "Advanced":
                                ret = userRights.Advanced;
                                break;
                            case "Normal":
                                ret = userRights.Normal;
                                break;
                            case "Semi-ignored":
                                ret = userRights.Semiignored;
                                break;
                            case "Ignored":
                                ret = userRights.Ignored;
                                break;
                            default:
                                ret = userRights.Normal;
                                break;
                        }

                        _accessLevel = ret;
                        _retrieved_accessLevel = true;
                        return ret;
                    }
                    else
                    {
                        return _accessLevel;
                    }
                }
                catch ( Exception ex )
                {
                    GlobalFunctions.ErrorLog( ex , System.Reflection.MethodInfo.GetCurrentMethod());
                }
          
                    return userRights.Normal;
                
            }
            set
            {
                throw new NotImplementedException( );                
            }
        }
        
        public enum userRights
        {
            Superuser = 2,
            Advanced = 1,
            Normal = 0,
            Semiignored = -1,
            Ignored = -2
        }

        
    }
}

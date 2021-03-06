﻿using System.Linq;
using SteamKit2;
using SteamTrade;

namespace SteamBot
{
    /// <summary>
    /// The abstract base class for users of SteamBot that will allow a user
    /// to extend the functionality of the Bot.
    /// </summary>
    public abstract class UserHandler
    {
        protected Bot Bot;
        protected SteamID OtherSID;

        public UserHandler (Bot bot, SteamID sid)
        {
            Bot = bot;
            OtherSID = sid;
        }

        /// <summary>
        /// Gets the Bot's current trade.
        /// </summary>
        /// <value>
        /// The current trade.
        /// </value>
        public Trade Trade
        {
            get
            {
                return Bot.CurrentTrade; 
            }
        }
        
        /// <summary>
        /// Gets the log the bot uses for convenience.
        /// </summary>
        protected Log Log
        {
            get { return Bot.log; }
        }
        
        /// <summary>
        /// Gets a value indicating whether the other user is admin.
        /// </summary>
        /// <value>
        /// <c>true</c> if the other user is a configured admin; otherwise, <c>false</c>.
        /// </value>
        protected bool IsAdmin
        {
            get { return Bot.Admins.Contains (OtherSID); }
        }

        /// <summary>
        /// Called when a the user adds the bot as a friend.
        /// </summary>
        /// <returns>
        /// Whether to accept.
        /// </returns>
        public abstract bool OnFriendAdd ();

        /// <summary>
        /// NOT IMPLEMENTED.
        /// </summary>
        /// <remarks>This will probably be implemented as the opposite of <see cref="OnFriendAdd"/>.</remarks>
        public abstract void OnFriendRemove ();

        /// <summary>
        /// Called whenever a message is sent to the bot.
        /// This is limited to regular and emote messages.
        /// </summary>
        public abstract void OnMessage (string message, EChatEntryType type);

        /// <summary>
        /// Called whenever a user requests a trade.
        /// </summary>
        /// <returns>
        /// Whether to accept the request.
        /// </returns>
        public abstract bool OnTradeRequest ();

        #region Trade events
        // see the various events in SteamTrade.Trade for descriptions of these handlers.

        public abstract void OnTradeError (string error);

        public abstract void OnTradeTimeout ();

        public virtual void OnTradeClose ()
        {
            Bot.log.Warn ("[USERHANDLER] TRADE CLOSED");
            Bot.CloseTrade ();
        }

        public abstract void OnTradeInit ();

        public abstract void OnTradeAddItem (Schema.Item schemaItem, Inventory.Item inventoryItem);

        public abstract void OnTradeRemoveItem (Schema.Item schemaItem, Inventory.Item inventoryItem);

        public abstract void OnTradeMessage (string message);

        public abstract void OnTradeReady (bool ready);

        public abstract void OnTradeAccept ();

        #endregion Trade events
    }
}

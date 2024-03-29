﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDeX.Games.Omi.Core
{
    public static class ActionCodes
    {
        public const string UpdatePlayers = "AC1";
        public const string RequestJoinAsPlayer = "AC2";
        public const string JoinPlayerFailed = "AC3";
        public const string JoinPlayerSuccess = "AC4";
        public const string ShuffleCards = "AC5";
        public const string EngineDataChanged = "AC6";
        public const string TeamDataChanged = "AC7";
        public const string SetTrump = "AC8";
        public const string ShareCards = "AC9";
        public const string PlaceCard = "AC10";
        public const string Rename = "AC11";
        public const string UpdateMembers = "AC12";
        public const string Message = "AC13";
    }
}

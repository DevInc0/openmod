﻿using OpenMod.API.Prioritization;
using OpenMod.Core.Permissions;
using OpenMod.Unturned.Entities;
using OpenMod.Unturned.Users;

namespace OpenMod.Unturned.Permissions
{
    [Priority(Priority = Priority.High)]
    public class UnturnedAdminPermissionCheckProvider : AlwaysGrantPermissionCheckProvider
    {
        public UnturnedAdminPermissionCheckProvider() : base(actor => actor is UnturnedPlayer user && user.SteamPlayer.isAdmin)
        {
        }
    }
}

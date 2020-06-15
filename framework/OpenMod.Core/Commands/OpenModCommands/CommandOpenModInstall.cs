﻿using System;
using System.Drawing;
using System.Threading.Tasks;
using OpenMod.Core.Plugins.NuGet;
using OpenMod.NuGet;

namespace OpenMod.Core.Commands.OpenModCommands
{
    [Command("install")]
    [CommandAlias("i")]
    [CommandDescription("Install plugins from NuGet")]
    [CommandSyntax("<id> [version] [-Pre]")]
    [CommandParent(typeof(CommandOpenMod))]
    public class CommandOpenModInstall : Command
    {
        private readonly NuGetPluginAssembliesSource m_NuGetPlugins;

        public CommandOpenModInstall(IServiceProvider serviceProvider, NuGetPluginAssembliesSource nuGetPlugins) : base(serviceProvider)
        {
            m_NuGetPlugins = nuGetPlugins;
        }

        protected override async Task OnExecuteAsync()
        {
            var args = Context.Parameters.ToList();

            string packageName = Context.Parameters[0];
            string version = null;
            bool isPre = false;

            if (args.Contains("-Pre"))
            {
                isPre = true;
                args.Remove("-Pre");
            }

            if (args.Count > 1)
            {
                version = args[1];
            }

            await Context.Actor.PrintMessageAsync($"Installing {packageName}...", Color.White);

            var result = await m_NuGetPlugins.InstallPackageAsync(packageName, version, isPre);
            if (result.Code != NuGetInstallCode.Success)
            {
                await Context.Actor.PrintMessageAsync($"Failed to install \"{packageName}\": " + result.Code, Color.DarkRed);
                return;
            }

            await Context.Actor.PrintMessageAsync($"Successfully installed {result.Identity.Id} v{result.Identity.Version}.", Color.DarkGreen);
            await Context.Actor.PrintMessageAsync($"To complete installation, please reload OpenMod with /openmod reload.", Color.White);
        }
    }
}
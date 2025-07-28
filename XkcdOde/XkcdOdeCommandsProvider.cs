// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace XkcdOde;

public partial class XkcdOdeCommandsProvider : CommandProvider
{
    private readonly ICommandItem[] _commands;

    public XkcdOdeCommandsProvider()
    {
        DisplayName = "xkcd";
        Icon = IconHelpers.FromRelativePath("Assets\\StoreLogo.png");
        _commands = [
            new CommandItem(new XkcdOdePage()) {
                Title = DisplayName,
                MoreCommands = [
                    new CommandContextItem(new OpenUrlCommand("http://xkcd.com") { Name = "Browser" }),
                ] },
        ];
    }

    public override ICommandItem[] TopLevelCommands()
    {
        return _commands;
    }

}

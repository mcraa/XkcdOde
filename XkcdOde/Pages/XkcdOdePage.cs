// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using System;
using System.Diagnostics.CodeAnalysis;

namespace XkcdOde;

internal sealed partial class XkcdOdePage : ContentPage
{
    private XkcdResponse? content;
    private readonly Random _rnd = new();
    private readonly int? _num;

    public XkcdOdePage(int? num = null)
    {
        Icon = IconHelpers.FromRelativePath("Assets\\StoreLogo.png");
        Title = "xkcd";
        Name = "Open";
        _num = num;

        if (num != null)
        {
            var next = _rnd.Next(num.Value);
            content = XkcdService.GetXkcdResponse(next);
        }
        else
        {
            content = XkcdService.GetXkcdResponse(null);
        }

    }

    private AnonymousCommand GetOpenCommand
    {
        get
        {
            return new(() => { 
                ShellHelpers.OpenInShell($"http://xkcd.com/{content.num}"); 
            }) { Name = "Open browser", Result = CommandResult.KeepOpen() };
        }
    }

    public override IContextItem[] Commands
    {
        get
        {
            if (_num != null)
            {
                return [
                    new CommandContextItem(
                        new AnonymousCommand(
                            action: () => {
                                content = XkcdService.GetXkcdResponse(_rnd.Next(_num.Value));
                                RaiseItemsChanged();
                            })
                        { Result = CommandResult.KeepOpen(), Name = "Random" }
                    ),
                    new CommandContextItem(GetOpenCommand)
                ];
            }
            else
            {
                return [
                    new CommandContextItem(new XkcdOdePage(content.num) { Name = "Random" })
                ];
            }
        }
    }

    public override IContent[] GetContent()
    {
        if (content != null)
        {
            return [new MarkdownContent(
            $"""
            ![{content.alt}]({content.img}) 
            """)];
        }

        return [new MarkdownContent("Something fishy")];
    }

}

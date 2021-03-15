using System.ComponentModel;
using Terraria.ModLoader.Config;

public class ClientConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ClientSide;

    [Header("[i:3615] [C/3CFFC8:Client Configuration]")]

    [DefaultValue(true)]
    [Label("Superintendent Item Holograms")]
    [Tooltip("When playing the Superintendent Minigame, cycled items will show their sprite right above their name.")]
    public bool contraSprite;

    [DefaultValue(true)]
    [Label("Vanity Valkyrie Help")]
    [Tooltip("Shows helpful information on navigating the Vanity Valkyrie UI when it's open")]
    public bool vanityItemHelp;

    [Header("[i:3625] [C/DC3C00:Developer Configuration]")]

    [DefaultValue(false)]
    [Label("Superintendent Text Chat Logging")]
    [Tooltip("Displays a chat message whenever a mod theme is skipped/contrabande is taken or submitted")]
    public bool algorithmoMessage;

    [DefaultValue(false)]
    [Label("Show Testing Tablet UI")]
    public bool tabletUI;
}
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

    [Header("[i:3093] [C/E1AF96:Storage Item Configuration]")]

    [DefaultValue(true)]
    [Label("Right-Click Storage View")]
    [Tooltip("Changes how storage items are opened:\nRight-Clicking once will view the contents of the clicked bag\nRight-Clicking again will open the bag")]
    public bool bagDoubleClick;

    [DefaultValue(true)]
    [Label("Bag Item Highlight")]
    [Tooltip("After opening a bag, all items from the bag to your inventory will be highlighted\nRight-Clicking on a bag will send all highlighted items to that bag (if space is available)")]
    public bool bagTagBoolean;

    [Range(0, 1)]
    [DefaultValue(0.5f)]
    [Label("Bag Item Highlight Duration")]
    [Tooltip("After opening a bag, all items from the bag to your inventory will be highlighted\nChanging this config will modify how long this highlight will last for before expiring\nAbove config must be set to true for this config to have purpose")]
    public float bagTagExpire;

    [Header("[i:3625] [C/DC3C00:Developer Configuration]")]

    [DefaultValue(false)]
    [Label("Superintendent Text Chat Logging")]
    [Tooltip("Displays a chat message whenever a mod theme is skipped/contrabande is taken or submitted")]
    public bool algorithmoMessage;

    [DefaultValue(false)]
    [Label("Show Testing Tablet UI")]
    public bool tabletUI;

    [Header("[i:2799] [C/4173FF:Miscellaneous Configuration]")]

    [DefaultValue(true)]
    [Label("Generate Tarr Pits (TEST)")]
    [Tooltip("TESTING FEATURE:\nGenerates 'Tarr Pits' biomes, though it is incomplete")]
    public bool tarrPits;
}
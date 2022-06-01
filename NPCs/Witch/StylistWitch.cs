using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using CastledsContent.Items.Summon.DistortedFlask;
using CastledsContent.Items.Storage;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Utilities;
using System.Collections.Generic;
using Terraria.GameContent.Events;
using Terraria.GameContent.UI;

namespace CastledsContent.NPCs.Witch
{
    [AutoloadHead]
    public class StylistWitch : ModNPC
    {
        int[] reaction = new int[2];
        int atckDelay;
        readonly Dictionary<int, int> pirateStuff = new Dictionary<int, int>
        {
            { 0, ItemID.Cutlass },
            { 1, ItemID.PirateStaff },
            { 2, ItemID.GoldRing },
            { 3, ItemID.DiscountCard },
            { 4, ItemID.LuckyCoin },
            { 5, ItemID.CoinGun }
        };
        public override bool Autoload(ref string name)
        {
            name = "StylistWitch";
            return mod.Properties.Autoload;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arachnich");
            Main.npcFrameCount[npc.type] = 23;
            NPCID.Sets.ExtraFramesCount[npc.type] = 5;
            NPCID.Sets.AttackFrameCount[npc.type] = 4;
            NPCID.Sets.DangerDetectRange[npc.type] = 400;
            NPCID.Sets.AttackType[npc.type] = 0;
            NPCID.Sets.AttackTime[npc.type] = 40;
            NPCID.Sets.AttackAverageChance[npc.type] = 10;
        }

        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.friendly = true;
            npc.width = 40;
            npc.height = 54;
            npc.aiStyle = 7;
            npc.defDefense = 15;
            npc.damage = 10;
            npc.lifeMax = 250;
            npc.knockBackResist = 0.5f;
            npc.HitSound = SoundID.NPCHit29;
            npc.DeathSound = SoundID.NPCDeath33;
            npc.knockBackResist = 1f;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.Venom] = true;
            animationType = NPCID.Stylist;
        }
        public override string[] AltTextures
        {
            get
            {
                return new string[] { "CastledsContent/NPCs/Witch/StylistWitch_Alt" };
            }
        }
        public override void HitEffect(int hitDirection, double damage) { Main.PlaySound(SoundID.NPCHit, npc.position); }
        public override bool CanTownNPCSpawn(int numTownNPCs, int money) => false;
        public override bool UsesPartyHat() => false;
        public override string TownNPCName()
        {
            switch (WorldGen.genRand.Next(6))
            {
                case 1:
                    return "Serv";
                case 2:
                    return "Ashley";
                case 3:
                    return "Widow";
                case 4:
                    return "Silverweaver";
                case 5:
                    return "Cackle";
                default:
                    return "Arachni the IV";
            }
        }
        public override string GetChat()
        {
            Player player = Main.LocalPlayer;
            CastledPlayer mP = player.GetModPlayer<CastledPlayer>();
            WeightedRandom<string> chat = new WeightedRandom<string>();
            chat.Add("Word of advice, never use a popsicle for farming: it's too small to even collect any crops, and it even melts after a half hour for pete sake!", 1.25);
            chat.Add("Could you stop tugging on that? Yeesh, that's the only thing I can wear.", 0.25);
            chat.Add("What's the secret with these flasks? I mix every ingredient with an exception of every kind.", 1.5);
            chat.Add("I once visited the castle of my former ruler for ONE night, and when I ate one of the sugar biscuits in the queen's kitchen, it really felt like I stayed there for much longer than ONE night.", 1);
            if (NPC.downedSlimeKing)
                chat.Add("I once tried to create a potion with different colored gels, and well, it was a blur after I added the third gel.", 1.5);
            if (Main.hardMode)
            {
                chat.Add("Those pixies out there remind me of the people from a place that my former ruler used to groan about. Difference being that those balls of yellow light are freakishly strong...", 0.75);
                chat.Add("A sand witch? I think I have seen a flying sand witch in the desert before, though it was really hard to see during that time.", 0.5);
                if (NPC.downedPlantBoss && !mP.witchEgg && reaction[1] < 1)
                {
                    chat.Add((player.Male ? "Mister" : "Mistress") + ", I do belive that my soon-to-be son has been abducted by a pumpkin, is that true?", 2);
                    if (HasEgg())
                    {
                        mP.witchEgg = true;
                        player.QuickSpawnItem(ItemID.PlatinumCoin);
                        return "There he is! I suppose you're a better person than I thought... Have this coin; it's all I can repay you with.";
                    }
                }
            }
            if (Main.dayTime)
            {
                chat.Add("I don't know what they were talking about, it looks great during the morning!", 1.25);
                chat.Add("Good morning, refined sugar cane. Heh-", 0.33);
            }
            if (!Main.dayTime)
            {
                chat.Add("What in the world are you doing past your curfew, sweetie?", 0.95);
                chat.Add("Good evening, bee vomit... HAHAHAHA", 0.33);
            }
            if (Main.bloodMoon)
            {
                chat.Add("You're arachnophobic? Then go away. I don't want to cause another heart attack tonight.", 1.25);
                chat.Add("*manical laugh followed by a maniacal coughing fit*", 1.25);
                chat.Add("It is the night of bloodlust... yes, I will harvest the organs of everyone! FOR CA- oh, um... greetings?", 1.25);
                if (NPCName(NPCID.Nurse) != string.Empty)
                chat.Add($"I visited {NPCName(NPCID.Nurse)} tonight, and frankly, she look terrified when she tried to call 'The Stylist' and didn't get an answer.", 1.25);
            }
            if (NPCName(NPCID.DD2Bartender) != string.Empty)
                chat.Add($"{NPCName(NPCID.DD2Bartender)} told me a tale about the commander of an ethereal army: he's never fought without his armor on... what a wuss.", 0.75);
            if (NPCName(NPCID.ArmsDealer) != string.Empty)
                chat.Add($"A bayonetta? You mean those spear things that {NPCName(NPCID.ArmsDealer)} always puts under his rifle when he sees me?", 0.45);
            if (NPCName(NPCID.Dryad) != string.Empty)
                chat.Add("A dryad? Anyone can be a druid. Do they specialize in nature magic? Are they at least 200 years old? Can they do thi- oops, that's a hex, my apollogies.", 0.75);
            if (mP.witchQuest[3] < 4)
            {
                mP.witchQuest[3] = 4;
                    return $"Oh, damn it, my favorite disguise is ruined! I suppose I should be more calm next time- oh right, you. I suppose I'll tell a bit about myself: my name is {npc.FullName}, and I am the fourth descendant of the Arachne dynasty, originating from a far away kingdom. I am half-human, half-black recluse, and I can eat you inside-out. Don't worry, I won't do that last part... you don't look that appetizing anyways.";
            }
            if (BirthdayParty.PartyIsUp)
            {
                chat.Add("Yeah, this hairpin is made of pure obsidian, why do you ask?", 1.25);
                chat.Add("A party hat? Pfft. I'm not gonna insult myself.", 1.25);
                chat.Add("I asked if anyone wanted me to perform any party tricks. Everyone just looked at me with a sheepish face afterwards.", 0.75);
            }
            if (mP.witchQuest[1] > 2)
                chat.Add("Do you remember that ruby that you gave me? It reminded me of my home; my mortifying, wretched home... Forget I said anything.", 0.75);
            if (mP.witchQuest[1] > 7)
                chat.Add("Thanks for all of those supplies. I can finally get around to making a real project now.", 1.25);
            if (reaction[1] > 0)
            {
                List<string> chatter = new List<string>();
                chatter.Add("I can't plead to god, and I can't plead to satan... my time is over.");
                chatter.Add("Are you 'pretending' to hold that? If so, haha, VERY funny.");
                chatter.Add("You're just like the reset of them, huh?");
                if (mP.witchQuest[1] > 5)
                    chatter.Add("After everything you've done to help me... You're doing this?");
                if (mP.witchEgg)
                    chatter.Add("Let me live! I have a family now!");
                return Main.rand.Next(chatter);
            }
            string NPCName(int type)
            {
                foreach (NPC n in Main.npc)
                    if (n.type == type)
                        return n.GivenName;
                return string.Empty;
            }
            bool HasEgg()
            {
                for (int a = 0; a < player.inventory.Length - 9; a++)
                {
                    if (ItemExists(player.inventory[a]) && player.inventory[a].type == ItemID.SpiderEgg)
                    {
                        player.inventory[a].SetDefaults(ItemID.None);
                        return true;
                    }
                    bool ItemExists(Item item) => item != null && !item.IsAir;
                }
                return false;
            }
            return chat;
        }

        public override void AI()
        {
            Player player = Main.LocalPlayer;
            if (CastledWorld.pirateIndex == null || CastledWorld.pirateIndex.Length < 1)
            {
                CastledWorld.pirateIndex = new int[2];
                for (int a = 0; a < CastledWorld.pirateIndex.Length; a++)
                    CastledWorld.pirateIndex[a] = 0;
            }
            CastledWorld.pirateIndex[0] += Main.dayRate;
            if (player.HeldItem.type == ItemID.HolyWater)
            {
                if (LineOfSight(player))
                {
                    npc.ai[0] = 0;
                    npc.ai[1] = 300;
                    npc.direction = player.position.X <= npc.position.X ? -1 : 1;
                    if (reaction[1] < 1)
                    {
                        EmoteBubble.NewBubble(EmoteID.EmotionAlert, new WorldUIAnchor(npc), 60);
                        reaction[1] = 1;
                        reaction[0] = 300;
                    }
                    if (reaction[1] == 2 && Main.rand.NextBool(90))
                        Dust.NewDust(npc.position, npc.width, npc.height, 172, 0, 4);
                }

            }
            if ((player.HeldItem.type != ItemID.HolyWater || !LineOfSight(player)) && reaction[1] > 0)
                if (reaction[0]-- < 1)
                    reaction[1] = 0;

            if (EmptyQuests())
            {
                player.GetModPlayer<CastledPlayer>().witchQuest = new int[4];
                for (int a = 0; a < player.GetModPlayer<CastledPlayer>().witchQuest.Length; a++)
                    player.GetModPlayer<CastledPlayer>().witchQuest[a] = 0;
            }
            bool EmptyQuests() => player.GetModPlayer<CastledPlayer>().witchQuest.Length < 1;
            if (atckDelay-- < 0)
                atckDelay = 0;
            if (CastledWorld.pirateIndex[0] > Main.dayLength)
            {
                CastledWorld.pirateIndex[1]++;
                CastledWorld.pirateIndex[0] = 0;
            }
            if (CastledWorld.pirateIndex[1] > 5)
                CastledWorld.pirateIndex[1] = 0;
        }
        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
            button2 = "Favor";
        }
        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            string allQuestsComplete = "I appreciate the kindness, but I don't need any help anymore. You helped me with everything, and all that I have is available for you to buy!";
            Player player = Main.LocalPlayer;
            CastledPlayer modPlayer = player.GetModPlayer<CastledPlayer>();
            int[] quest = modPlayer.witchQuest;
            if (firstButton)
                shop = true;
            else
            {
                if (reaction[1] < 1)
                {
                    if (quest[0] < 1)
                    {
                        Main.PlaySound(SoundID.Chat);
                        Main.npcChatText = ChatMessageQuest();
                        quest[0]++;
                        if (quest[1] < 5)
                            player.QuickSpawnItem(ModContent.ItemType<DistortedFlask>(), 3);
                        string ChatMessageQuest()
                        {
                            switch (quest[1])
                            {
                                case 0:
                                    return "So you want to help me, huh? That's nice of you, but you're going to have to make quite a few sacrifices to get the things that I would request. To start off, I need an aged skull, mainly to act as a jug. Throw one of these flasks at [c/FFFF00:someone who is experienced with explosive munitions], and get rid of the waste afterwards. Stand back after you do so, otherwise, you yourself may be waste.";
                                case 1:
                                    return "You wanna help again? Alright. I need a book. Not just any book, but a book held by [c/FFFF00:a knowledgeable person]. I know that he loves archaeology, so what better than to make his dream come true and turn him into an archaeologist? Throw one of these flasks at him, and forcefully take the book from him. There's a side effect that occurs when he loses his hat though, so just be prepared when that happens.";
                                case 2:
                                    return "Back for more? Alright. I'll give you a bit of a break this time: I need a ruby so that I can shine a very specific amount of light onto a cauldron. I believe that [c/FFFF00:a female druid] usually has one for a hairclip. Throw one of my flasks at one of the dryads, then relieve them of their misery. There's no special last stands that they do, they just simply succumb.";
                                case 3:
                                    return "Ah, you're here. I have recently found out that the material that those 'illegal gun parts' have a very reactive property with rose petals. Conveniently enough, I also learned that [c/FFFF00:a gunsmith and a doctor] are planning to go out on a date, a perfect opportunity to do some corrupting, and take their stuff! They both have to stand close to each other in order for them to be affected, however.";
                                case 4:
                                    return "Welcome back. I just found out that a similar situation like the last one, but with [c/FFFF00:goblin and human mechanics] is going on. This time, I don't need ingredients, but rather tools so that I can be very specific with my ingredients. You know what to do; throw a flask at both of them, and bring some powerful artillery with you to break the bond.";
                                case 5:
                                    return "If you haven't, you're gonna have to find [c/FFFF00:a wizard]. It might be impossible to find him if this world isn't imbued with spirits, though. I have a raging surge of spite going through me, and I need you to help me relieve it. Throw one of these flasks at the old chum, and get ready for a fight. Bring me his hat if you win so I can keep it as a trophy!";
                                case 6:
                                    return "I don't think I need anymore ingredients from now on, it's really either just for utility, or for my own sake. Speaking of utility, I need some fabric to make a glove; these flasks become really hard to hold at times! I think that an eyepatch from [c/FFFF00:a powerful pirate] would do quite nicely. You know the drill; throw a flask at friendly pirate, then kill the villanous pirate. He doesn't have any special properties, I think.";
                                case 7:
                                    return "You know what? I feel like doing something good for once. I once met [c/FFFF00:a dead fellow with an interest in yo-yoing] underground, and I asked him what his greatest desire was. He said that he always wanted to be a wizard. I really think that the flask will do just the thing, but this time, there won't be side effects. Just throw it at him, and if he has any thank you gifts, bring them to me; I was the one that told you to turn him into a wizard, after all.";
                            }
                            return allQuestsComplete;
                        }
                    }
                    else
                    {
                        if (HasQuestItem())
                        {
                            Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Achievement"));
                            Main.npcChatText = ChatMessageQuestComplete();
                            Rewards();
                            quest[1]++;
                            quest[0] = 0;
                            quest[2] = 0;
                            void Rewards()
                            {
                                switch (quest[1])
                                {
                                    case 0:
                                        {
                                            player.QuickSpawnItem(ItemID.GoldCoin, 5);
                                            player.QuickSpawnItem(ItemID.SilverCoin, 50);
                                            player.QuickSpawnItem(Main.rand.NextBool(1) ? ItemID.MiningShirt : ItemID.MiningPants);
                                        }
                                        break;
                                    case 1:
                                        {
                                            player.QuickSpawnItem(ItemID.GoldCoin, 7);
                                            player.QuickSpawnItem(ItemID.SilverCoin, 50);
                                            player.QuickSpawnItem(ItemID.CordageGuide);
                                            player.QuickSpawnItem(ItemID.JungleGrassSeeds, 15);
                                        }
                                        break;
                                    case 2:
                                        {
                                            player.QuickSpawnItem(ItemID.GoldCoin, 10);
                                            player.QuickSpawnItem(ItemID.StaffofRegrowth);
                                        }
                                        break;
                                    case 3:
                                        {
                                            player.QuickSpawnItem(ItemID.GoldCoin, 17);
                                            player.QuickSpawnItem(ItemID.SilverCoin, 50);
                                            player.QuickSpawnItem(ItemID.Boomstick);
                                            player.QuickSpawnItem(ItemID.BandofRegeneration);
                                        }
                                        break;
                                    case 4:
                                        {
                                            player.QuickSpawnItem(ItemID.GoldCoin, 25);
                                            player.QuickSpawnItem(ItemID.WebSlinger);
                                            player.QuickSpawnItem(ModContent.ItemType<Items.Storage.Cardboard>(), 20);
                                        }
                                        break;
                                    case 5:
                                        {
                                            player.QuickSpawnItem(ItemID.GoldCoin, 40);
                                            player.QuickSpawnItem(ItemID.SoulofNight, 15);
                                            player.QuickSpawnItem(ItemID.SoulofLight, 15);
                                            player.QuickSpawnItem(ModContent.ItemType<Items.Storage.SealedVaccuum>(), 5);
                                        }
                                        break;
                                    case 6:
                                        {
                                            player.QuickSpawnItem(ItemID.GoldCoin, 75);
                                            player.QuickSpawnItem(ModContent.ItemType<PirateVanity>());
                                        }
                                        break;
                                    case 7:
                                        {
                                            player.QuickSpawnItem(ItemID.PlatinumCoin);
                                            player.QuickSpawnItem(ItemID.GoldCoin, 50);
                                            player.QuickSpawnItem(ModContent.ItemType<PotionPouch>());
                                        }
                                        break;
                                }
                            }
                            string ChatMessageQuestComplete()
                            {
                                switch (quest[1])
                                {
                                    case 0:
                                        return "Not too bad. This skull has the right texture to hold some flasks. I also found a piece of clothing at the blast site, you can have it; I don't need it. Still, thanks for the help: I'll keep some more things available in my shop each time you help me.";
                                    case 1:
                                        return "Once again, great work. I needed to learn more about cultivation, and I think I now know enough. You can have it back. Also, there seemed to be a lot of jungle seeds in there, he must have liked exploring then.";
                                    case 2:
                                        return "Ah, you have it! It's so pristine as well, oh my... I suppose that I should give you something to help you with gardening with such a nature theme, here, have this Staff of Regrowth. As well as that, I'll be providing a discount on any flasks that you buy, just in case you have to do some... 'retrieval' often.";
                                    case 3:
                                        return "I hate to just ruin a relationship like that, but man was it worth it! These parts seem to be made of unknown materials. When mixed with those rose petals, it creates a bright blue color! I found a shotgun in the gunsmith's appartment, and a heart ring in the doctor's appartment. I don't know about you, but that seemed to be a very close call.";
                                    case 4:
                                        return "Wow, you're incredible at this! You really did all of these favors for me? I'll give you something special; this is a gift that I was given back at my home, I think it's best that you to have it now. And the cardboard? You can make some packages with them to store your stuff; it's pretty neat.";
                                    case 5:
                                        return "Let me tell you, it feels great to not have anymore competition. Hm, what's this? Whoa! That's a lot of souls, was he stashing them? For what? There also seems to be floating balls of gas. I do know that those can be used for a lot of storage, and even those packages that I said before. Again, thank you so much for the help!";
                                    case 6:
                                        return "There you are! And there's the eyepatch. I think that throwing that flask was really more of just getting rid of his cool more than anything, what a rough guy. Speaking of which, I found this bag in his room, it smells really awful. Maybe put a clothes line clip on your nose while you open it.";
                                    case 7:
                                        return "He gave you a heart crystal? I thought that he would just give you a star but a heart crystal? I really do think that I should change my ways... Maybe everything isn't just about evil, and whatnot. I think that this is deservant of a special reward; this is my pouch that I use for storing potions. You can put potions inside and use them directly from the bag.";
                                }
                                return allQuestsComplete;
                            }
                        }
                        else
                        {
                            Main.npcChatText = ChatMessageOngoingQuest();
                            if (quest[2]++ > 1)
                                quest[2] = 1;
                            string ChatMessageOngoingQuest()
                            {
                                if (quest[2] < 1)
                                {
                                    switch (quest[1])
                                    {
                                        case 0:
                                            return $"Need a reminder, huh? I need you to throw a Flask of Distortion [i/1:{ModContent.ItemType<DistortedFlask>()}] at [c/FFFF00:someone who is experienced with explosives], then get rid of the waste. Make sure you're careful while doing so!";
                                        case 1:
                                            return $"Alright, so you have to throw one of those glitched flasks [i/1:{ModContent.ItemType<DistortedFlask>()}] at [c/FFFF00:someone with orange hair, and an arrogant attitude.] Kill him afterwards, and make sure that you're ready to jump so that you won't get flattened.";
                                        case 2:
                                            return $"A bit tired, eh? I need you to throw a Flask of Distortion [i/1:{ModContent.ItemType<DistortedFlask>()}] at [c/FFFF00:an ancient idol of nature], then knock their hairclip off.";
                                        case 3:
                                            return $"Tough crowd, huh? You gotta throw a glitch in a bottle [i/1:{ModContent.ItemType<DistortedFlask>()}] at [c/FFFF00:a gun enthusiast and a nurse] who are standing close to each other. They will fall in love if they are able to see each other directly, making them really sturdy. Bring back anything special that they drop.";
                                        case 4:
                                            return $"Tough crowd, huh? You gotta throw a glitch in a bottle [i/1:{ModContent.ItemType<DistortedFlask>()}] at [c/FFFF00:a greedy goblin and a dextrous woman] who are standing close to each other. They will fall in love if they are able to see each other directly, making them really sturdy. Bring back anything special that they drop.";
                                        case 5:
                                            return $"Having trouble finding [c/FFFF00:the wizard]? He should be found tied up underground if it's hardmode. Once you find him, throw a flask [i/1:{ModContent.ItemType<DistortedFlask>()}] at him, and kill the remanining rune wizard. Bring me his hat!";
                                        case 6:
                                            return $"Arr ye having trouble? Search [c/FFFF00:for gold (the pirate)], then throw a sample of me rum [i/1:{ModContent.ItemType<DistortedFlask>()}] at 'im. Pull out ye' arrrtillery and peg him until his eyepatch falls.";
                                        case 7:
                                            return $"Having trouble finding [c/FFFF00:the skeleton merchant]? I believe that he sometimes appears below the surface. Do him a favor and throw a Flask of Distortion [i/1:{ModContent.ItemType<DistortedFlask>()}] at him. He would appreciate being a wizard. ";
                                    }
                                }
                                if (quest[2] >= 1)
                                {
                                    switch (quest[1])
                                    {
                                        case 0:
                                            return $"I don't think it should be that hard to remember, now. I need you to throw a Flask of Distortion [i/1:{ModContent.ItemType<DistortedFlask>()}] at [c/FFFF00:a dwarf who loves dynamite]. Get rid of the remaining skeleton, and stand far back after you do so; he'll lob grenades as a last stand.";
                                        case 1:
                                            return $"Come on, now. All you have to do is throw one of my flasks [i/1:{ModContent.ItemType<DistortedFlask>()}] at [c/FFFF00:a suitable sacrifice for a flesh wall], then retreive the book that he drops. He will drop two boulders upon losing his hat, so be wary of that.";
                                        case 2:
                                            return $"Maybe it's more than drousiness... You have to throw one of my glitch bottles [i/1:{ModContent.ItemType<DistortedFlask>()}] at [c/FFFF00:a female druid], and then kill the resulting nymph. Bring back the hairclip, or rather, ruby that they drop";
                                        case 3:
                                            return (player.Male ? "Lad" : "Lass") + $", do you have gel in your ears? Throw a Flask of Distortion [i/1:{ModContent.ItemType<DistortedFlask>()}] at [c/FFFF00:an arms dealer and a nurse] who are standing close to each other. They will form a strong bond if they can see each other.";
                                        case 4:
                                            return (player.Male ? "Lad" : "Lass") + $", do you have gel in your ears? Throw a Flask of Distortion [i/1:{ModContent.ItemType<DistortedFlask>()}] at [c/FFFF00:a goblin tinkerer and a mechanic] who are standing close to each other. They will form a strong bond if they can see each other.";
                                        case 5:
                                            return $"Hmm, are you sure that he isn't in your town? Make sure that you know where [c/FFFF00:the wizard] is, then throw some of my medicine [i/1:{ModContent.ItemType<DistortedFlask>()}] at him. Give him a taste of your medicine, then bring me his hat!";
                                        case 6:
                                            return $"Don't make me walk you off the plank! Bring your eyes to the attention of [c/FFFF00:the cap'n (a friendly pirate)], then throw some of my rum [i/1:{ModContent.ItemType<DistortedFlask>()}] at 'im. Then swab the deck with his face and bring me his eyepatch.";
                                        case 7:
                                            return $"Don't leave me hanging! You can find [c/FFFF00:the skeleton merchant] deep below. His greatest wish is to be a wizard, so I'm pretty sure that the effects of my flask [i/1:{ModContent.ItemType<DistortedFlask>()}] on him would grant that wish.";
                                    }
                                }
                                return allQuestsComplete;
                            }
                        }
                    }
                }
                else
                    Main.npcChatText = "NO! I don't want help! Please, just leave me alone!";
            }
            bool HasQuestItem()
            {
                if (quest[1] != 3 || quest[1] != 4)
                {
                    for (int a = 0; a < player.inventory.Length - 9; a++)
                    {
                        if (ItemExists(player.inventory[a]) && player.inventory[a].type == QuestItem() && player.inventory[a].GetGlobalItem<DistortItem>().distorted)
                        {
                            player.inventory[a].SetDefaults(ItemID.None);
                            return true;
                        }
                    }
                }
                if (quest[1] == 3)
                {
                    Item parts = new Item();
                    Item rose = new Item();
                    for (int a = 0; a < player.inventory.Length - 9; a++)
                    {
                        if (ItemExists(player.inventory[a]) && player.inventory[a].GetGlobalItem<DistortItem>().distorted)
                        {
                            if (player.inventory[a].type == ItemID.IllegalGunParts)
                                parts = player.inventory[a];
                            if (player.inventory[a].type == ItemID.JungleRose)
                                rose = player.inventory[a];
                        }
                    }
                    if (parts != null && !parts.IsAir && rose != null && !rose.IsAir)
                    {
                        parts.SetDefaults(ItemID.None);
                        rose.SetDefaults(ItemID.None);
                        return true;
                    }
                }
                if (quest[1] == 4)
                {
                    Item goggles = new Item();
                    Item ruler = new Item();
                    for (int a = 0; a < player.inventory.Length - 9; a++)
                    {
                        if (ItemExists(player.inventory[a]) && player.inventory[a].GetGlobalItem<DistortItem>().distorted)
                        {
                            if (player.inventory[a].type == ItemID.Goggles)
                                goggles = player.inventory[a];
                            if (player.inventory[a].type == ItemID.LaserRuler)
                                ruler = player.inventory[a];
                        }
                    }
                    if (goggles != null && !goggles.IsAir && ruler != null && !ruler.IsAir)
                    {
                        goggles.SetDefaults(ItemID.None);
                        ruler.SetDefaults(ItemID.None);
                        return true;
                    }
                }
                return false;
                int QuestItem()
                {
                    switch(quest[1])
                    {
                        case 0:
                            return ItemID.Skull;
                        case 1:
                            return ItemID.Book;
                        case 2:
                            return ItemID.Ruby;
                        case 5:
                            return ItemID.WizardsHat;
                        case 6:
                            return ItemID.EyePatch;
                        case 7:
                            return ItemID.LifeCrystal;
                    }
                    return 0;
                }
            }
            bool ItemExists(Item item) => item != null && !item.IsAir;
        }
        public override void NPCLoot()
        {
            if (Main.hardMode && Main.rand.NextBool(3))
                Item.NewItem(npc.position, ItemID.SpiderFang, Main.rand.Next(3, 5));
            Gore.NewGore(npc.position, Vector2.Zero, mod.GetGoreSlot("Gores/WitchCloakG"));
            for (int a = 0; a < Main.rand.Next(3, 5); a++)
                Gore.NewGore(npc.position, Vector2.Zero, Main.rand.Next(61, 63));
        }
        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            CastledPlayer player = Main.LocalPlayer.GetModPlayer<CastledPlayer>();
            int value = player.witchQuest[1] > 2 ? 25000 : 50000; 
            shop.item[0].SetDefaults(ModContent.ItemType<DistortedFlask>());
            shop.item[0].value = value;
            shop.item[1].SetDefaults(ItemID.Cobweb);
            shop.item[1].value = 125;
            if (player.witchQuest[1] > 0)
            {
                shop.item[10].SetDefaults(ItemID.BlinkrootSeeds);
                shop.item[10].value = shop.item[10].value * 4;
                shop.item[20].SetDefaults(ItemID.Bottle);
                shop.item[20].value = shop.item[20].value * 4;
            }
            if (player.witchQuest[1] > 1)
            {
                shop.item[11].SetDefaults(ItemID.MoonglowSeeds);
                shop.item[11].value = shop.item[11].value * 4;
                shop.item[21].SetDefaults(ItemID.Book);
                shop.item[21].value = shop.item[21].value * 4;
            }
            if (player.witchQuest[1] > 2)
            {
                shop.item[12].SetDefaults(ItemID.DaybloomSeeds);
                shop.item[12].value = shop.item[12].value * 4;
                shop.item[22].SetDefaults(ItemID.Stinger);
                shop.item[22].value = shop.item[22].value * 4;
            }
            if (player.witchQuest[1] > 3)
            {
                shop.item[13].SetDefaults(ItemID.WaterleafSeeds);
                shop.item[13].value = shop.item[13].value * 4;
                shop.item[14].SetDefaults(ItemID.ShiverthornSeeds);
                shop.item[14].value = shop.item[14].value * 4;
            }
            if (player.witchQuest[1] > 4)
            {
                shop.item[15].SetDefaults(ItemID.DeathweedSeeds);
                shop.item[15].value = shop.item[15].value * 4;
                shop.item[16].SetDefaults(ItemID.FireblossomSeeds);
                shop.item[16].value = shop.item[16].value * 4;
            }
            if (player.witchQuest[1] > 5)
            {
                shop.item[23].SetDefaults(ItemID.SpiderFang);
                shop.item[23].value = shop.item[23].value * 4;
                shop.item[24].SetDefaults(ItemID.PoisonStaff);
                shop.item[24].value = shop.item[24].value * 4;
            }
            if (player.witchQuest[1] > 6)
            {
                shop.item[25].SetDefaults(pirateStuff[CastledWorld.pirateIndex[1]]);
                shop.item[25].value = shop.item[25].value * 4;
            }
            if (player.witchQuest[1] > 7)
            {
                List<int> potions = new List<int>
                {
                    ItemID.GenderChangePotion,
                    ItemID.TeleportationPotion,
                    ItemID.WormholePotion,
                    ItemID.RecallPotion,
                    ItemID.StinkPotion,
                    ItemID.LovePotion,
                    ItemID.CratePotion,
                    ItemID.SonarPotion,
                    ItemID.FishingPotion
                };
                for(int a = 30; a < 38; a++)
                {
                    shop.item[a].SetDefaults(potions[38 - a]);
                    shop.item[a].value = shop.item[a].value * 4;
                }
            }
        }
        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            if (atckDelay < 1)
            {
                Main.PlaySound(SoundID.Item71, npc.position);
                Vector4 range = new Vector4(npc.position.X + (npc.direction == -1 ? -400 : 400), npc.position.Y - 50, npc.position.X, npc.position.Y + 15);
                foreach (NPC n in Main.npc)
                {
                    if (!n.friendly && !n.townNPC)
                    {
                        if (WithinX() && npc.position.Y > range.Y && npc.position.Y < range.W && Collision.CanHitLine(npc.position, npc.width, npc.height, n.position, n.width, n.height))
                        {
                            n.StrikeNPC(Main.hardMode ? 80 : 40, 0f, npc.direction);
                            n.AddBuff(Main.hardMode ? BuffID.Venom : BuffID.Poisoned, 600);
                        }
                    }
                    bool WithinX()
                    {
                        if (npc.direction == -1)
                            return n.position.X > range.X && n.position.X <= range.Z;
                        if (npc.direction == 1)
                            return n.position.X < range.X && n.position.X >= range.Z;
                        return false;
                    }
                }
                atckDelay = 40;
            }
            //int proj = Projectile.NewProjectile(new Vector2(npc.position.X + npc.direction == -1 ? -30 : 30, npc.position.Y), Vector2.Zero, ModContent.ProjectileType<SliceProj>(), 0, 0f);
            //Main.projectile[proj].ai[0] = Main.hardMode ? 80 : 40;
            //Main.projectile[proj].ai[1] = npc.direction;
        }
        bool LineOfSight(Entity e)
        {
            Vector4 range = new Vector4(npc.position.X + (npc.direction == -1 ? -500 : 500), npc.position.Y - 250, npc.position.X + (npc.direction == -1 ? 50 : -50), npc.position.Y + 250);
            if (!npc.HasBuff(BuffID.OnFire) && Collision.CanHitLine(npc.position, npc.width, npc.height, e.position, e.width, e.height))
            {
                if (npc.direction == -1)
                    return e.position.X > range.X && e.position.X <= range.Z;
                if (npc.direction == 1)
                    return e.position.X < range.X && e.position.X >= range.Z;
            }
            return false;
        }
        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 0f;
            gravityCorrection = 0f;
            /*
              for (int a = 0; a < witchQuest.Length; a++)
              witchQuest[a] = 0;
            */
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (reaction[1] > 0)
            {
                Player player = Main.LocalPlayer;
                int FrameY()
                {
                    if (LineOfSight(player))
                    {
                        if ((npc.position - player.position).Length() < 150)
                        {
                            reaction[1] = 2;
                            if (player.position.Y < npc.position.Y - 100)
                                return 162;
                            if (player.position.Y > npc.position.Y + 100)
                                return 270;
                            return 216;
                        }
                        else
                        {
                            reaction[1] = 1;
                            if (player.position.Y < npc.position.Y - 100)
                                return 0;
                            if (player.position.Y > npc.position.Y + 100)
                                return 108;
                            return 54;
                        }
                    }
                    return 324;
                }
                Rectangle frame = new Rectangle(0, FrameY(), npc.frame.Width, npc.frame.Height);
                spriteBatch.Draw(ModContent.GetTexture("CastledsContent/NPCs/Witch/FaceRealization"), npc.Center - Main.screenPosition, frame, drawColor, 0, new Vector2(20, 25), 1f, npc.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            }
        }
    }
    public class PirateVanity : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deckhand's Spare Clothes");
            Tooltip.SetDefault("'For when scurvvy is the least of your problems'\nContains many pirate-related vanity items.");
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 22;
            item.maxStack = 99;
            item.value = 125000;
            item.rare = ItemRarityID.LightRed;
        }
        public override bool CanRightClick() => true;
        public override void RightClick(Player player)
        {
            List<int> items = new List<int>
            {
                ItemID.BuccaneerBandana,
                ItemID.BuccaneerShirt,
                ItemID.BuccaneerPants,
                ItemID.SailorHat,
                ItemID.SailorShirt,
                ItemID.SailorPants,
                ItemID.EyePatch
            };
            foreach(int i in items)
                player.QuickSpawnItem(i);
        }
    }
    public class PotionPouch : BagItem
    {
        public static int[] operations;
        public override int BagLimit { get { return 25; } }
        public override List<string> EquipTooltips
        {
            get
            {
                return new List<string>
                {
                    "Right-Click while holding shift to instantly use all consumables inside."
                };
            }
        }
        public override string Texture => "CastledsContent/NPCs/Witch/PotionPouch";
        public override void SetStaticDefaults() { DisplayName.SetDefault("Alchemist's Pouch"); Tooltip.SetDefault("Alternatively, hold control instead of shift for a 'Safe' usage; not using any items that grants buffs you already have."); }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 22;
            item.value = 250000;
            item.rare = ItemRarityID.Yellow;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            Texture2D texture = Main.itemTexture[item.type];
            Color[] array = new Color[texture.Width * texture.Height];
            texture.GetData(array);
            if (operations == null)
            {
                Array.Resize(ref operations, array.Length);
                for (int a = 0; a < array.Length; a++)
                {
                    if (array[a] == Color.White)
                        operations[a] = 1;
                    else
                        operations[a] = 0;
                }
            }
            else
            {
                for (int a = 0; a < array.Length; a++)
                    if (operations[a] == 1)
                        array[a] = new Color(255, Main.DiscoG - 125, 25);
                texture.SetData(array);
            }
            return base.GetAlpha(lightColor);
        }
    }
}
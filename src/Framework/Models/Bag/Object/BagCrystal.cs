using PokemonFramework.EmulatorBridge.InputInterface;
using PokemonFramework.EmulatorBridge.MemoryInterface;
using System;
using System.Collections.Generic;

namespace PokemonFramework.Framework.Models.Bag.Object
{
    public class BagCrystal : IBagObject
    {
        internal override MemoryQuery CurrentPocketQuery => new(0xCF65, 1, MemoryDomain.WRAM);
        internal override MemoryQuery SelectedItemQuery => new(0xD95C, 1, MemoryDomain.WRAM);

        internal override IReadOnlyList<IPocketObject> Pockets
            {
                get => [
                    pocketFactory.CreateObject(PocketType.Item, 0xD892, 0),
                    pocketFactory.CreateObject(PocketType.Ball, 0xD8D7, 1),
                    pocketFactory.CreateObject(PocketType.KeyItem, 0xD8BC, 2),
                    pocketFactory.CreateObject(PocketType.TMHM, 0xD859, 3),
                    ];
            }

        public override bool NavigateToPocket(PocketType pocket)
        {
            for (int i = 0; i < Pockets.Count; i++)
            {
                if (GetCurrentPocket().PocketType == pocket) return true;
                API.Input.Press([Button.RIGHT], duration: 8);
            }
            return false;
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        public override bool UseBestBall()
        {
            throw new NotImplementedException();
        }

        public override bool UseItem(PocketType pocket, Item.Item item)
        {
            throw new NotImplementedException();
        }
    }
}

﻿using System;
using System.Linq;
using Alex.API.Graphics.Typography;
using Alex.API.Gui.Dialogs;
using Alex.API.Gui.Elements;
using Alex.API.Gui.Graphics;
using Alex.API.Input;
using Alex.API.Utils;
using Alex.Entities;
using Alex.Graphics.Models.Entity;
using Alex.Gui.Elements;
using Alex.Gui.Elements.Inventory;
using Alex.Items;
using Alex.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NLog;
using RocketUI;
using GuiCursorEventArgs = Alex.API.Gui.Events.GuiCursorEventArgs;

namespace Alex.Gui.Dialogs.Containers
{
    public class GuiPlayerInventoryDialog : GuiInventoryBase
    {
        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();
        
        protected Player Player { get; }
        protected Inventory Inventory { get; }


        private InventoryContainerItem[] _guiHotBarInventoryItems;
        private GuiEntityModelView _playerEntityModelView;

        private GuiAutoUpdatingTextElement _debug;

        private const int ItemSize = 16;
        
        private InventoryContainerItem CraftingOutput { get; }
        public GuiPlayerInventoryDialog(Player player, Inventory inventory)
        {
            Player = player;
            Inventory = inventory;

            // Subscribe to events

            _guiHotBarInventoryItems = new InventoryContainerItem[inventory?.SlotCount ?? 0];

            if(_guiHotBarInventoryItems.Length != 46) throw new ArgumentOutOfRangeException(nameof(inventory), inventory?.SlotCount ?? 0, "Expected player inventory containing 46 slots.");

            ContentContainer.Background = GuiTextures.InventoryPlayerBackground;
            ContentContainer.BackgroundOverlay = null;
            
            ContentContainer.Width = ContentContainer.MinWidth = ContentContainer.MaxWidth = 176;
            ContentContainer.Height = ContentContainer.MinHeight = ContentContainer.MaxHeight = 166;
            
            SetFixedSize(176, 166);
            
            ContentContainer.AutoSizeMode = AutoSizeMode.None;

            var modelRenderer = player.ModelRenderer;
            var mob = new PlayerMob(player.Name, player.Level, player.Network,
                player.ModelRenderer.Texture)
            {
                ModelRenderer = modelRenderer,
            };

            ContentContainer.AddChild(_playerEntityModelView =
                new GuiEntityModelView(mob)
                {
                    Margin = new Thickness(7, 25),
                    Width = 49,
                    Height = 70,
                    Anchor = Alignment.TopLeft,
                    AutoSizeMode = AutoSizeMode.None,
                    Background = null,
                    BackgroundOverlay =  null
                });
            
            Color color = Color.Blue;

            foreach (var slot in AddSlots(8, 84, 9, 27, 9, 0x00))
            {
             //   slot.HighlightedBackground = new Microsoft.Xna.Framework.Color(color, 0.5f);
                slot.Item = Inventory[slot.InventoryIndex];
            }

            color = Color.Aqua;
            foreach (var slot in AddSlots(8, 142, 9, 9, 0, 0))
            {
               // slot.HighlightedBackground = new Microsoft.Xna.Framework.Color(color, 0.5f);
                slot.Item = Inventory[slot.InventoryIndex];
            }

            foreach (var slot in AddSlots(8, 8, 1, 4, 0, 120))
            {
                var  inventoryIndex = slot.InventoryIndex;
                Item item = new ItemAir();
                
                switch (slot.InventoryIndex)
                {
                    case 0:
                        item = Inventory.Helmet;
                        inventoryIndex = Inventory.HelmetSlot;
                        break;
                    case 1:
                        item = Inventory.Chestplate;
                        inventoryIndex = Inventory.ChestSlot;
                        break;
                    case 2:
                        item = Inventory.Leggings;
                        inventoryIndex = Inventory.LeggingsSlot;
                        break;
                    case 3:
                        item = Inventory.Boots;
                        inventoryIndex = Inventory.BootsSlot;
                        break;
                }
                
              //  slot.HighlightedBackground = new Microsoft.Xna.Framework.Color(Color.Red, 0.5f);
                slot.Item = item;
                slot.InventoryIndex = inventoryIndex;
            }

            foreach (var slot in AddSlots(98, 18, 2, 4, 41, 0))
            {
                slot.Item = Inventory[slot.InventoryIndex];
              //  slot.HighlightedBackground = new Microsoft.Xna.Framework.Color(Color.Purple, 0.5f);
            }

            CraftingOutput = AddSlot(154, 28, 45, 0);
          //  CraftingOutput.HighlightedBackground = new Microsoft.Xna.Framework.Color(Color.Purple, 0.5f);
           
            /*var shieldSlot = new InventoryContainerItem()
            {
                HighlightedBackground = new Microsoft.Xna.Framework.Color(Color.Orange, 0.5f),
                Anchor = Alignment.TopLeft,
                Margin =  new Thickness(61, 76),
                AutoSizeMode = AutoSizeMode.None,
                Item = Inventory[40],
                InventoryIndex = 40
            };
                
            ContentContainer.AddChild(shieldSlot);*/
        }

        private readonly float _playerViewDepth = -512.0f;
        private bool _mouseDown = false;
        protected override void OnUpdate(GameTime gameTime)
        {
            base.OnUpdate(gameTime);

            var mousePos = Alex.Instance.InputManager.CursorInputListener.GetCursorPosition();
            var playerPos = _playerEntityModelView.RenderBounds.Center.ToVector2();

            var mouseDelta = (new Vector3(playerPos.X, playerPos.Y, _playerViewDepth) - new Vector3(mousePos.X, mousePos.Y, 0.0f));
            mouseDelta.Normalize();

            var headYaw = (float)mouseDelta.GetYaw();
            var pitch = (float)mouseDelta.GetPitch();
            var yaw = (float)headYaw;

            _playerEntityModelView.SetEntityRotation(-yaw, pitch, -headYaw);

            if (Inventory != null)
            {
                _playerEntityModelView.Entity.ShowItemInHand = true;
                
                _playerEntityModelView.Entity.Inventory[Inventory.SelectedSlot] = Inventory[Inventory.SelectedSlot];
                _playerEntityModelView.Entity.Inventory.MainHand = Inventory.MainHand;
                _playerEntityModelView.Entity.Inventory.SelectedSlot = Inventory.SelectedSlot;
            }
        }
        
        protected override void OnSlotChanged(InventoryContainerItem slot, Item item)
        {
            Inventory.SetSlot(slot.InventoryIndex, item, true);
            // Inventory[slot.InventoryIndex] = item;
        }
        
        protected override void OnCursorItemChanged(InventoryContainerItem slot, Item item)
        {
            Inventory.SetCursor(item, true);
           // Inventory.Cursor = item;
        }

        protected override void OnInit(IGuiRenderer renderer)
        {
            base.OnInit(renderer);
        }
    }
}

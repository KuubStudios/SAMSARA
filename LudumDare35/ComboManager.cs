using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LudumDare35
{
    public class KeyComboNode
    {
        private readonly Dictionary<Keys, KeyComboNode> subNodes = new Dictionary<Keys, KeyComboNode>();
        private Action comboAction;

        public bool HasAction()
        {
            return comboAction != null;
        }

        public bool Invoke()
        {
            comboAction?.Invoke();
            return HasAction();
        }

        public void AddCombo(Keys[] keys, Action func)
        {
            if (keys.Length == 0)
            {
                if (comboAction != null)
                {
                    throw new InvalidOperationException("Duplicate keycombos");
                }

                if (subNodes.Count != 0)
                {
                    throw new InvalidOperationException("Overlapping keycombos");
                }

                comboAction = func;
                return;
            }

            KeyComboNode node;
            if (subNodes.TryGetValue(keys[0], out node))
            {
                if (node.HasAction())
                {
                    throw new InvalidOperationException("Overlapping keycombos");
                }

                node.AddCombo(keys.Skip(1).ToArray(), func);
            }
            else
            {
                node = new KeyComboNode();
                node.AddCombo(keys.Skip(1).ToArray(), func);
                subNodes[keys[0]] = node;
            }
        }

        public bool KeyPressed(Keys key, out KeyComboNode node)
        {
            Console.WriteLine("Pressed {0}", key);

            if (subNodes.TryGetValue(key, out node))
            {
                if (node.Invoke())
                {
                    Console.WriteLine("invoking");
                    node = null;
                    return false;
                }
            }

            return true;
        }
    }

    public class ComboManager
    {
        public event EventHandler ComboReset;

        private readonly KeyComboNode rootNode = new KeyComboNode();
        private KeyComboNode currentCombo;
        private double now, lastKey;

        public void Update(GameTime gameTime)
        {
            now = gameTime.TotalGameTime.TotalMilliseconds;

            if (now - lastKey > 1000 && currentCombo != null)
            {
                ComboReset?.Invoke(this, EventArgs.Empty);
                currentCombo = null;
            }
        }

        public void KeyPressed(Keys key)
        {
            lastKey = now;

            if (currentCombo == null)
            {
                Console.WriteLine("new combo");
            }

            KeyComboNode node = currentCombo ?? rootNode;
            KeyComboNode newNode;
            bool end = node.KeyPressed(key, out newNode);
            if (end && currentCombo != null && newNode == null)
            {
                Console.WriteLine("combo ended");
                ComboReset?.Invoke(this, EventArgs.Empty);

                currentCombo = null;
                KeyPressed(key);
            }
            else
            {
                currentCombo = newNode;
            }
        }

        public void AddCombo(Keys[] keys, Action func)
        {
            rootNode.AddCombo(keys, func);
        }
    }
}

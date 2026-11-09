using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Bachelor.Core
{
    public class InputState
    {
        private readonly HashSet<Keys> _consumed = new();
        private KeyboardState _current;
        private KeyboardState _previous;

        public void Update()
        {
            _previous = _current;
            _current = Keyboard.GetState();
            _consumed.Clear();
        }

        public bool IsKeyDown(Keys key) => _current.IsKeyDown(key);

        public bool IsKeyJustPressed(Keys key) =>
            !_consumed.Contains(key) && _current.IsKeyDown(key) && _previous.IsKeyUp(key);

        public bool IsKeyJustPressed(Keys a, Keys b) =>
            IsKeyJustPressed(a) || IsKeyJustPressed(b);

        public void Consume(Keys key) => _consumed.Add(key);
    }
}
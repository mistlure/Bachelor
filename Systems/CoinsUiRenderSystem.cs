using Bachelor.Components;
using Bachelor.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bachelor.Systems
{
    public class CoinsUiRenderSystem
    {
        private const float Margin = 10f;

        private int _lastBalance = int.MinValue;
        private string _coinsText = string.Empty;

        public void Draw(World world, SpriteBatch spriteBatch, SpriteFont font, int screenWidth)
        {
            if (!world.TryGetSingleton<WalletComponent>(out var walletEntity))
            {
                return;
            }

            var walletOpt = world.TryGetComponent<WalletComponent>(walletEntity);
            if (!walletOpt.HasValue)
            {
                return;
            }

            int balance = walletOpt.Value.Balance;
            if (balance != _lastBalance)
            {
                _lastBalance = balance;
                _coinsText = $"Coins: {balance}";
            }

            Vector2 textSize = font.MeasureString(_coinsText);
            Vector2 position = new Vector2(screenWidth - textSize.X - Margin, Margin);

            spriteBatch.DrawString(font, _coinsText, position, Color.Black);
        }
    }
}

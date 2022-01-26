using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Tutorial.Matrix;
using Tutorial;

namespace OpusSample
{
    /// <summary>
    /// 基底 Game クラスから派生した、ゲームのメイン クラスです。
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        BasicEffect effect;

        // dirtyFlagサンプル
        MeshGraphNode mesh1;
        MeshGraphNode mesh2;
        MeshGraphNode mesh3;
        MeshGraphNode mesh4;

        // オブジェクトプールサンプル
        ParticlePool particlePool;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// ゲームが実行を開始する前に必要な初期化を行います。
        /// ここで、必要なサービスを照会して、関連するグラフィック以外のコンテンツを
        /// 読み込むことができます。base.Initialize を呼び出すと、使用するすべての
        /// コンポーネントが列挙されるとともに、初期化されます。
        /// </summary>
        protected override void Initialize()
        {
            // TODO: ここに初期化ロジックを追加します。
            InputManager.Initialize();

            mesh1 = new MeshGraphNode();
            mesh2 = new MeshGraphNode(mesh1);
            mesh2.Local = new MeshTransform(Tutorial.Vector.Vector2.CreateTrancerate(new Tutorial.Vector.Vector2(0, -0.1f)));
            mesh3 = new MeshGraphNode(mesh2);
            mesh3.Local = new MeshTransform(Tutorial.Vector.Vector2.CreateTrancerate(new Tutorial.Vector.Vector2(0, -0.1f)));
            mesh4 = new MeshGraphNode(mesh3);
            mesh4.Local = new MeshTransform(Tutorial.Vector.Vector2.CreateTrancerate(new Tutorial.Vector.Vector2(0, -0.1f)));

            particlePool = new ParticlePool();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent はゲームごとに 1 回呼び出され、ここですべてのコンテンツを
        /// 読み込みます。
        /// </summary>
        protected override void LoadContent()
        {
            // 新規の SpriteBatch を作成します。これはテクスチャーの描画に使用できます。
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: this.Content クラスを使用して、ゲームのコンテンツを読み込みます。
            effect = new BasicEffect(GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent はゲームごとに 1 回呼び出され、ここですべてのコンテンツを
        /// アンロードします。
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: ここで ContentManager 以外のすべてのコンテンツをアンロードします。
        }

        /// <summary>
        /// ワールドの更新、衝突判定、入力値の取得、オーディオの再生などの
        /// ゲーム ロジックを、実行します。
        /// </summary>
        /// <param name="gameTime">ゲームの瞬間的なタイミング情報</param>
        protected override void Update(GameTime gameTime)
        {
            // ゲームの終了条件をチェックします。
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: ここにゲームのアップデート ロジックを追加します。
            InputManager.Update();

            if (InputManager.IsKeyDown(Keys.D1) || InputManager.IsJustKeyDown(Keys.F1))
            {
                mesh1.GetLocal().positionMatrix *= Matrix3x3.CreateRotation((float)Math.PI / 180 * (90 * (float)gameTime.ElapsedGameTime.TotalSeconds));
            }
            if (InputManager.IsKeyDown(Keys.D2) || InputManager.IsJustKeyDown(Keys.F2))
            {
                mesh2.GetLocal().positionMatrix *= Matrix3x3.CreateRotation((float)Math.PI / 180 * (90 * (float)gameTime.ElapsedGameTime.TotalSeconds));
            }

            if (InputManager.IsKeyDown(Keys.D3) || InputManager.IsJustKeyDown(Keys.F3))
            {
                mesh3.GetLocal().positionMatrix *= Matrix3x3.CreateRotation((float)Math.PI / 180 * (90 * (float)gameTime.ElapsedGameTime.TotalSeconds));
            }
            if (InputManager.IsKeyDown(Keys.D4) || InputManager.IsJustKeyDown(Keys.F4))
            {
                mesh4.GetLocal().positionMatrix *= Matrix3x3.CreateRotation((float)Math.PI / 180 * (90 * (float)gameTime.ElapsedGameTime.TotalSeconds));
            }


            if (InputManager.IsKeyDown(Keys.Space))
            {
                particlePool.create(new Tutorial.Vector.Vector3(0.5f, 0.5f, 1), Tutorial.Vector.Vector3.UP, 2);
            }

            base.Update(gameTime);
        }
        
        /// <summary>
        /// ゲームが自身を描画するためのメソッドです。
        /// </summary>
        /// <param name="gameTime">ゲームの瞬間的なタイミング情報</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            mesh1.render(MeshTransform.origin, mesh1.Dirty);

            particlePool.animate(gameTime);

            // TODO: ここに描画コードを追加します。
            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                DrawingEvent(GraphicsDevice);
            }

            base.Draw(gameTime);
        }
        public delegate void drawing(GraphicsDevice gra);
        public static event drawing DrawingEvent;
    }
}

# MatrixとDirtyFlagの練習。と、オブジェクトプールの練習

dirtyFragを覚えようと思ったら、サンプルが行列計算を用いていたのでMatrixから覚えることにしました。使用言語はC#です。

追加でオブジェクトプールもやってみてます。

覚えたてほやほやのメモみたいなものです。

Game Programming Patterns ソフトウェア開発の問題解決メニューを参考にしています。

https://www.amazon.co.jp/dp/B015R0M8W0/ref=dp-kindle-redirect?_encoding=UTF8&btkr=1

サンプルプログラムはMicrosoft XNAを使用しています。

## Matrixについて

まず、Matrix(和名:行列)で出来ることは、人が肩を回すと肘が動く、肘が動くと手が動くのような、ものがくっついているものと一緒に動く挙動を数学的にいい感じに行ってくれるというものです。
Unityなどではこの関係を親子と言い、肩が肘の親、肘が手の親といった呼び方がされます。

各部位がそれぞれMatrixを持っており、そこにはその部位の親に対しての位置、回転、大きさの情報が一つのMatrixに入っています。

- [Matrix3x3.cs](https://github.com/1827009/Tutorial/blob/1153547c6977cf8de67eecf6392f8188cfee38eb/OpusSample/OpusSample/OpusSample/Tutorial/Matrix/Matrix3x3.cs)

### 計算方法

たいていのゲームエンジンなどには既存のMatrixクラスとか構造体があるのでそれを使えばいいですが、仕組みだけでも知っておきましょう。
扱うことができれば、一つの物体自身のMatrixに親のMatrixを掛けてやるだけで親の回転、移動、大きさがきちんと反映されるという何とも都合のいい計算方法です。

Matrixは3d空間なら4x4の16個の数値で構成された情報です。今回の私のサンプルでは2d空間を想定するため3x3のMatrix構造体を作成しています。
3x3も4x4もそんなに差はないです。空間軸に対して1つ多いのはその方が計算の都合がいいからだそうです。
```
// 宣言
public Matrix3x3(float m11, float m12, float m13,
                 float m21, float m22, float m23,
                 float m31, float m32, float m33)
        {
            M11 = m11; M12 = m12; M13 = m13;
            M21 = m21; M22 = m22; M23 = m23;
            M31 = m31; M32 = m32; M33 = m33;
        }
```

この大量の数値を計算と言われても、となりますが、Matrixは普通に数学なのでググってみるとより詳しい解説がたくさん乗っています。
計算順などが心底覚えずらかったので調べましょう。理解を深められればプログラム上でまとめてしまえるので常に思い出す必要がなくなるのは幸いです。

```
// 掛け算
        public static Matrix3x3 Multiply(Matrix3x3 matrix1, Matrix3x3 matrix2)
        {
            Matrix3x3 output;
            output.M11 = (matrix1.M11 * matrix2.M11) + (matrix1.M12 * matrix2.M21) + (matrix1.M13 * matrix2.M31);
            output.M12 = (matrix1.M11 * matrix2.M12) + (matrix1.M12 * matrix2.M22) + (matrix1.M13 * matrix2.M32);
            output.M13 = (matrix1.M11 * matrix2.M13) + (matrix1.M12 * matrix2.M23) + (matrix1.M13 * matrix2.M33);

            output.M21 = (matrix1.M21 * matrix2.M11) + (matrix1.M22 * matrix2.M21) + (matrix1.M23 * matrix2.M31);
            output.M22 = (matrix1.M21 * matrix2.M12) + (matrix1.M22 * matrix2.M22) + (matrix1.M23 * matrix2.M32);
            output.M23 = (matrix1.M21 * matrix2.M13) + (matrix1.M22 * matrix2.M23) + (matrix1.M23 * matrix2.M33);

            output.M31 = (matrix1.M31 * matrix2.M11) + (matrix1.M32 * matrix2.M21) + (matrix1.M33 * matrix2.M31);
            output.M32 = (matrix1.M31 * matrix2.M12) + (matrix1.M32 * matrix2.M22) + (matrix1.M33 * matrix2.M32);
            output.M33 = (matrix1.M31 * matrix2.M13) + (matrix1.M32 * matrix2.M23) + (matrix1.M33 * matrix2.M33);

            return output;
        }        
```

前述の通り親のMatrixが変更されたとき、子のMatrixに親のを掛けてやるとその変更に対していい感じに調整されてくれます。そのMatrixをさらに子にかけてやると…としていくと、
肩から指先まできれいに位置を計算できます。

"回転行列"を掛けてやると回転を、"平行移動行列"を掛けてやると移動を、"拡大縮小行列"を大きさを変形してくれます。それぞれどのような数値にすればいいかは各ワードでググりましょう。

## DirtyFlagについて

DirtyFlagは軽量化の技術の一つで、アクションゲーム等では常にものに対して更新がかかり、そのたびに位置を計算するのは割と計算リソースが持っていかれます。そんな場面とかで使うのがDirtyFlagです。

Dirty(汚れる)の名の通り、現在の情報が古くなる(変更が加わる)と必要なオブジェクトに対してのみ更新がかかり、常時すべての更新をしなくて済むようにします。

また、変更をフラグで管理することで変更されたものだけを最後に一括で処理を行い、変更の度に計算した場合の

肘が変更→肘の位置を計算→肘の変更を指先まで計算→肩が変更→肘に位置をまた計算→変更を指先までまた計算…

といった、計算個所の重複が起きなくなる利点もあります。

今回のサンプルはMatrixの親子構造で、肩が更新されたら指先まですべて更新されるが、肘を更新されても肩は更新されないといったシステムです。

- [MeshGraphNode.cs](https://github.com/1827009/Tutorial/blob/06fc3e6b3ef6dfb4926fc92a241681306ea3a61c/OpusSample/OpusSample/OpusSample/Tutorial/DirtyMesh/MeshGraphNode.cs)

## アルゴリズム

Matrixに変更を加える際、セッタでdirtyFlagを建て、描画時に親から子へ更新処理を再帰呼び出しすることで実現しています。
```
        /// <summary>
        /// プロパティで更新フラグを立てつつ変更
        /// </summary>
        public MeshTransform Local
        {
            get { return local_; }
            set
            {
                local_ = value;
                dirty_ = true;
            }
        }
```

更新メソッドの引数で親のフラグを渡し、子の更新メソッドでは親か自分のFlagが建っている場合に計算を行います。そこから子の更新メソッドにフラグを渡し…と末端まで更新していく構造です。
```
        public void render(MeshTransform parentWorld, bool dirty)
        {
            //親か子がオンなら
            dirty |= dirty_;
            if (dirty)
            {
                // 計算
                world_ = local_.combine(parentWorld);
                dirty_ = false;

                mesh.VertexUpdate(world_.positionMatrix);
            }

            // 子の更新確認へ
            for (int i = 0; i < children_.Count; i++)
            {
                children_[i].render(world_, dirty);
            }
        }
```
各物体の親に対するMatrixのほかに、計算結果Matrixをworld_に保存して更新がかからなかった場合にはそれを表示をしています。そのため、フラグは宣言時点でオンで開始して計算させます。
```
        private bool dirty_ = true;
```

あとはゲームループの描画メソッドに親のrenderを自身のフラグを入れれば更新してくれます。

# オブジェクトプール

前述の二つとは関連性は特にないです。追加で勉強した部分のメモ書きです。

さて、オブジェクトプールとは何かですが、

- 簡単に言えば無尽蔵に増えていく物体などを使用する際、何の制限も設けなければどこかでメモリをオーバーする

- 小さいメモリを確保し、それが解放され、そこの前後のメモリが埋まっているとき、隙間が小さすぎてほとんどデータが入らず、メモリの無駄になる場面

に対応するアルゴリズムです。

- [Particle.cs](OpusSample/OpusSample/OpusSample/Tutorial/Particle/Particle.cs)
- [ParticlePool.cs](OpusSample/OpusSample/OpusSample/Tutorial/Particle/ParticlePool.cs)

## アルゴリズム

配列を用いて先にメモリを確保しちゃおう、というに尽きます。ただし、メモリの参照を常に保持するのでガベージコレクションが機能しづらかったり、破棄をしっかりしておかないとエラーが出ます。

```
        public ParticlePool() {
            // 配列を作成
            particles_ = new Particle[POOL_SIZE];
            for (int i = 0; i < POOL_SIZE; i++)
            {
                particles_[i] = new Particle();
            }


            firstAvailable_ = particles_[0];
            for (int i = 0; i < POOL_SIZE-1; i++)
            {
                particles_[i].unionParticle.particle = particles_[i + 1];
            }
            particles_[POOL_SIZE - 1].unionParticle.particle = null;
        }
```
今回はパーティクルに使用し、一定までしかパーティクルを生成しないようにする、既存のパーティクルが消え次第、そこのメモリにパーティクルを再生させるという風にしています。

firstAvailable_に常に使われているメモリの先端の要素を割り当て、パーティクルを再生する際はそのメモリに再生しています。

firstAvailable_がすでにパーティクルが再生しているメモリ、またはIndexの外の場合に生成を止めています。
```
        public void create(Vector3 pos, Vector3 posVal, int lifetime)
        {
            // 足りなくなったらErrorを出す
            //Debug.Assert(firstAvailable_ != null);

            // 足りなくなったらparticleを生成しない
            if (firstAvailable_ != null)
            {
                // 空になっているリストの先を指定
                Particle newParticle = firstAvailable_;
                firstAvailable_ = newParticle.unionParticle.particle;

                newParticle.init(pos, posVal, lifetime);
            }
        }
        
        public void animate(Microsoft.Xna.Framework.GameTime time)
        {
            for (int i = 0; i < POOL_SIZE; i++)
            {
                // particleが死んだら
                if (particles_[i].animate(time))
                {
                    // 空にして、そこを先頭と記憶する
                    particles_[i].unionParticle.particle = firstAvailable_;
                    firstAvailable_ = particles_[i];

                    Console.WriteLine("今の先頭index：" + i);
                }
            }
        }
```

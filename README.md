# MatrixとDirtyFlagの練習

dirtyFragを覚えようと思ったら、サンプルが行列計算を用いていたのでMatrixから覚えることにしました。

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

この大量の数値を計算と言われても、となりますが、Matrixは普通に数学なのでググってみるとより詳しい解説がたくさん乗っています。
計算順などが心底覚えずらかったので調べましょう。理解を深められればプログラム上でまとめてしまえるので常に思い出す必要がなくなるのは幸いです。

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

更新メソッドの引数で親のフラグを渡し、子の更新メソッドでは親か自分のFlagが建っている場合に計算を行います。そこから子の更新メソッドにフラグを渡し…と末端まで更新していく構造です。

各物体の親に対するMatrixのほかに、計算結果Matrixを保存して更新がかからなかった場合にはそれを表示をしています。そのため、フラグは一番最初はオンで開始して計算させます。

# **MatrixとdirtyFlagの練習

dirtyFragを覚えようと思ったら、Unityで言う親子関係の変形が必要になったのでMatrixから覚えることにしました。

覚えたてほやほやのメモみたいなものです。

Game Programming Patterns ソフトウェア開発の問題解決メニューを参考にしています。

https://www.amazon.co.jp/dp/B015R0M8W0/ref=dp-kindle-redirect?_encoding=UTF8&btkr=1

## Matrixについて

まず、Matrixで出来ることとは、

人が肩を回すと肘が動く、肘が動くと手が動くのような、ものがくっついているものと一緒に動く挙動をプログラム的にいい感じに行ってくれるものです。
これで言うと肩が肘、肘が手の親といった具合です。

Matrixには親に対する位置とか回転とかを一括で記録することができ、そいつを親の位置に掛けてやることでいい感じの位置に移動することができます。

これを三角関数とかでやろうとするととてもめんどくさい上、計算リソースも持っていかれるので使うものだと思っています。

### 計算方法

ググってみると普通に数学なのでより詳しい解説がたくさん乗っています。が、私には難解でした。

たいていのゲームエンジンなどには既存のMatrixクラスとか構造体があるのでそれを使えばいいですが、仕組みだけでも知っておきましょう。

## DirtyFlagについて

DirtyFlagは軽量化の技術の一つで、アクションゲーム等では常にものに対して更新がかかり、そのたびに位置を計算するのは割と計算リソースが持っていかれます。そんな場面とかで使うのがDirtyFlagです。

簡単に言うと、毎度全物体の位置を計算せず、動いたものだけ計算しましょうといったもの。

親が動けば子も動くが、子だけ動いても親は動かない、といった場面で今回書いたアルゴリズムが機能します。

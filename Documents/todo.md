#### 1.事前設計
- [x] いくつかのサイトでブロック崩しを遊ぶ。
- [x] 画面サイズと座標サイズを決める。
- [x] シーンと画面遷移を決める。
- [x] プロジェクト開始 & git管理開始。

#### 2.仮動作プロトの作成
- [x] ステージの作成。
- [x] バーの動作。
- [x] ボールの動作。
- [x] 各種衝突時動作。
  - [x] ボールとブロック。
  - [x] ボールとゲームオーバー。
- [x] uGUIで、基本的なラベルを作成。
- [x] メインシーン内での状態遷移。

#### 3.本実装
- [x] プロトシーンの処理をprefab化して、Mainシーンに引っ越す。
- [x] シーン間の状態遷移。
- [ ] 面の選択を実現する。
  - [ ] 面の選択に応じて、メインゲーム初期化処理をスイッチできるようにする。
  - [ ] ステージを簡単に編集できるようにして、ソースの局所か外部ファイルに埋め込む。
- [x] ブロックに硬さを設ける。
- [x] スコア機能の実装。
- [x] ライフ機能の実装。
- [ ] テクスチャ・エフェクトにこだわる。
  - [ ] アセットストアでいい感じのアセットを探してみる。
  - [ ] 全体的なテーマの印象を決める。
  - [ ] 各種コンポーネントをおしゃれにする。
    - [ ] Lighting。
    - [ ] Ball。
    - [ ] Stage。
    - [ ] Block。
    - [ ] ステージ外。
- [ ] コンフィグ画面の作成
  - [ ] シーンの作成と遷移つなぎ込み。
  - [ ] 必要な変数を注入できるようにする。
- [ ] アイテム付与機能(余裕あれば)
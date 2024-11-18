## 公開作品
 - 説明: 3Dホラーシュミレーションゲーム
 - url: https://store.steampowered.com/app/2583920/Ep/

## 一部システムのスクリプト

### 使用技術
 - 言語：C#
 - エンジン：Unity

### 1. キャラクターコントロール
 - PlayerMove.cs :プレイヤーキャラクターの移動を制御し、移動速度や方向を管理します。
 - Raycast_Mouseover.cs :プレイヤーがマウスでオブジェクトにカーソルを合わせたときの挙動を管理するスクリプト。

### 2. NPCの会話インタラクション
 - TextController.cs :UIテキストの表示や更新を管理します。ゲーム内のメッセージ情報を表示するために使用します。
 - Test1.cs :プレイヤーとのインタラクションを通じて、セリフやキャラクター名を動的に変更し、ゲーム体験を向上させる役割を果たします。

![movie_003kairyou1](https://github.com/user-attachments/assets/17173f83-b0c8-4c09-ab67-053c0505221a)

### 3. アイテム管理
 - ItemDataBase.cs :アイテムのデータベースを管理し、アイテムの詳細情報を格納します。
 - ItemManager.cs :インベントリの表示管理と、アイテムの存在確認および取得を行うことで、プレイヤーがアイテムを効果的に管理できるようにします。
 - Item.cs :ゲーム内のアイテムを定義し、アイテムの特性や動作を管理するスクリプト。

### 4. オブジェクトのインタラクション
 - Obtainable.cs :プレイヤーが取得可能なアイテムを定義し、取得時の挙動を管理するスクリプト。

![movie_005kairyou1](https://github.com/user-attachments/assets/fb35e9e9-acb3-4739-aef4-16d5bd95dc45)


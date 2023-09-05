# 2_Binary

## Binary_Programの説明

特定のヘッダを持つバイナリファイルを読みこんで内容すべてをcsvファイルに出力する。

## 使用言語
C#

## 環境
VisualStudio2019

## 実行方法
コマンドプロンプトでbatファイルがあるディレクトリに移動し、  
任意のbatファイルを実行。 

## 各batファイルについて
`Binary_Program.bat`：assets\内の画像のリンクを引数として起動します。  
`Write_Program.bat`：引数なしで起動します。（batファイル側で入力）  
`None_Argument.bat`：引数なしで起動します。（exeファイル側で入力）

## データフォーマット

#### ヘッダフォーマット

ヘッダ部の情報を元に、データ部の１レコード分のデータサイズが決定されています。

```
int (4byte)    ヘッダーレコード数
--- ヘッダ部
byte (1byte)   フィールド型情報　※後述
string (xbyte) フィールド名
… 

int (4byte)     データレコード数
--- データ部
[data] ※ヘッダ準拠
…
```

##### フィールド型情報

```
None    = 0        // 非対応な型の場合.
Int     = 1        4byte
Long    = 2        8byte
Float   = 3        4byte
Double  = 4        8byte
String  = 5        xbyte
Bool    = 6        1byte　　　// 0 or 1
Byte    = 7        1byte
SByte   = 8        1byte
Short   = 9        2byte
UShort  = 10       2byte
```

###### String データの場合の拡張情報

String 型の場合、データ部は下記になります。

```
byte      …　文字データが存在しているか
[length]　…　文字列長
[data]　　…　文字データ
```

###### Long or Int データの場合の拡張情報

上記フィールド型情報の場合、格納されるデータ内容によって、データ部の情報が追加される場合があります。

- 追加型情報
```
AsType   200   // 200以下の値はLongWriteType部分に直接値を書き込む.
AsByte   201   // Byte型として読み替える.
AsInt    202   // Int型として読み替える.
AsLong   203   // Long型として読み替える.
AsShort  204   // Short型として読み替える.
```

データの値が 200 以下の場合 → そのままデータの値となります   
データの値が 200 以上の場合は、上記追加型情報に従いデータは下記のように配置されます

```
byte    …　1byte // 追加型情報 or 200 以下の数値データ
[data]  …　xbyte // byte or short or int or long
```
"# simpleProject" 

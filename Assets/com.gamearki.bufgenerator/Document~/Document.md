# 简介
这是一个基于二进制的序列化、反序列化协议，依赖 Roslyn 进行代码生成。

# 使用方式
==== SETP 1 写自定义的数据结构 ====
```
// 保存为 MyModel.cs, 类名和文件名要一致
public struct MyModel {
    public int value;
}
```
==== STEP 2 挂上特性 ====
```
using GameArki.BufferIOExtra;

[BufferIOMessageObject]
public struct MyModel {
    public int value;
}
```
==== STEP 3 调用生成 ====
```
string messageInputDir = "MyModel.cs 所在的目录";
GameArki.BufferIOExtraGenerator.GenModel(messageInputDir);

// 程序会在 MyModel.cs 上自动添加序列化方法以及继承一个 IBufferIOMessage<T> 接口
// IBufferIOMessage<T> 接口主要的方法有三个:
    void WriteTo(byte[] dst, ref int offset);
    void FromBytes(byte[] src, ref int offset);
    byte[] ToBytes();
```
==== STEP 4 序列化/反序列化 ====
```
// 序列化
MyModel model = new MyModel();
model.value = 3;
byte[] data = model.ToBytes();

// 反序列化
MyModel newModel = new Model();
int offset = 0;
newModel.FromBytes(data, ref offset); // newModel.value == 3
```
更多细节可见 GameArki.BufferIOExtra/Sample 目录和 Tests 目录
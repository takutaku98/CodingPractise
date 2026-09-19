namespace Practice;

// 動的配列(C# の List<T> 相当)を、内部の固定長配列 T[] だけで自作する。
// 制約: List<T> など BCL のコレクションは使わない(使ってよいのは T[] と Array.Copy まで)。
//
// 実装前に考えること:
//   - 容量が足りなくなったとき、何要素ぶん拡張するか? +1 ずつと 2 倍とで Add の計算量はどう変わるか(償却計算量)
//   - RemoveAt で削除したあと、要素をどう詰めるか。計算量は?
//   - 範囲外の index に対して何を投げるか(List<T> の挙動に合わせる)
public class DynamicArray<T>
{
    public int Count => throw new NotImplementedException();

    public T this[int index]
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public void Add(T item) => throw new NotImplementedException();

    public void RemoveAt(int index) => throw new NotImplementedException();
}

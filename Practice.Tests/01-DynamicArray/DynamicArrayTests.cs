using FsCheck.Xunit;

namespace Practice.Tests;

// 雛形: 実装したら Skip を外して green にする。
// 次の題材からは、このファイルを手本にテストも自分で書く。
public class DynamicArrayTests
{
    private const string Pending = "実装したら Skip を外す";

    [Fact(Skip = Pending)]
    public void Add_IncreasesCount_AndKeepsInsertionOrder()
    {
        var array = new DynamicArray<int>();
        array.Add(10);
        array.Add(20);
        array.Add(30);

        Assert.Equal(3, array.Count);
        Assert.Equal(10, array[0]);
        Assert.Equal(20, array[1]);
        Assert.Equal(30, array[2]);
    }

    [Fact(Skip = Pending)]
    public void Indexer_OutOfRange_Throws()
    {
        var array = new DynamicArray<int>();
        array.Add(1);

        Assert.Throws<ArgumentOutOfRangeException>(() => array[1]);
        Assert.Throws<ArgumentOutOfRangeException>(() => array[-1]);
    }

    // モデルベースのプロパティ: 標準の List<T> を「正解の模型」にして、自作版と挙動が一致することを確かめる。
    // BCL のコレクションは実装では使わないが、テストの比較対象としてはここで使ってよい。
    [Property(Skip = Pending)]
    public bool Add_MatchesListModel(int[] xs)
    {
        if (xs is null)
            return true;

        var actual = new DynamicArray<int>();
        var model = new List<int>();
        foreach (var x in xs)
        {
            actual.Add(x);
            model.Add(x);
        }

        return actual.Count == model.Count
            && Enumerable.Range(0, model.Count).All(i => actual[i] == model[i]);
    }

    // TODO(自分で書く): RemoveAt のテスト。List<T> をモデルにして、削除後の Count と全要素が一致することを確かめる。
}

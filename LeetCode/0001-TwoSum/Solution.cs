public class Solution {
    public int[] TwoSum(int[] nums, int target) {
        if (nums == null)
            return [-1, -1];

        // 「今まで見た値 → そのインデックス」を記録する辞書
        var complementDict = new Dictionary<int, int>();
        for (var i = 0; i < nums.Length; ++i) {
            // 自分と足して target になる相棒の値
            var complement = target - nums[i];
            if (complementDict.TryGetValue(complement, out var index1)) {
                // 相棒が過去に出ていた → そのインデックスと今のインデックスが答え
                return [index1, i];
            } else {
                // いなければ自分を登録して、後続の要素から見つけてもらう
                complementDict[nums[i]] = i;
            }
        }

        return [-1, -1];
    }
}

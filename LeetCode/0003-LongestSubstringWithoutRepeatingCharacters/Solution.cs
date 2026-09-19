namespace LeetCode.LongestSubstringWithoutRepeatingCharacters;

public class Solution {
    public int LengthOfLongestSubstring(string s) {
        // 「文字 → その文字が最後に出てきたインデックス」を記録する辞書
        var lastSeen = new Dictionary<char, int>();
        var left = 0;
        var maxLength = 0;

        for (var right = 0; right < s.Length; right++) {
            if (lastSeen.TryGetValue(s[right], out var lastIndex)) {
                // 重複が今のウィンドウの外(既に left より前)なら left は動かさない
                left = Math.Max(left, lastIndex + 1);
            }
            lastSeen[s[right]] = right;
            maxLength = Math.Max(maxLength, right - left + 1);
        }

        return maxLength;
    }
}

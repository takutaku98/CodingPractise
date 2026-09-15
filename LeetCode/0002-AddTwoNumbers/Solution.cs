namespace LeetCode.AddTwoNumbers;

// LeetCode 側で定義されている単方向リストのノード
public class ListNode(int val = 0, ListNode? next = null) {
    public int val = val;
    public ListNode? next = next;
}

public class Solution {
    public ListNode AddTwoNumbers(ListNode? l1, ListNode? l2) {
        // ダミーの先頭ノード。答えのリストはこの後ろに繋いでいき、最後に dummy.next を返す
        var dummy = new ListNode();
        // tail は「答えのリストの現在の末尾」。ノードを繋ぐたびに前へ進める
        var tail = dummy;
        // 前の桁から繰り上がってきた値(0 か 1)
        var carry = 0;

        // どちらかのリストが残っているか、繰り上がりが残っている限り続ける
        while (l1 != null || l2 != null || carry != 0) {
            // 短い方のリストが尽きたら、その桁は 0 として扱う
            var sum = (l1?.val ?? 0) + (l2?.val ?? 0) + carry;
            carry = sum / 10;                  // 次の桁への繰り上がり
            tail.next = new ListNode(sum % 10); // この桁の値
            tail = tail.next;                  // 末尾を進める(これを忘れると毎回上書きになる)

            l1 = l1?.next;
            l2 = l2?.next;
        }

        return dummy.next!;
    }
}

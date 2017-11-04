using UnityEngine;

// 이 인터페이스를 상속받는 클래스들은
// 실제 클래스 내용물이 무엇이든 상관 없이 OnDamage 함수를 무조건 만들어야 한다.

// 이 인터페이스를 상속받는 클래스들은
// 외부에서 무조건 "OnDamage" 함수는 가지고 있다고 판단할 수 있음.
public interface IDamageable
{
	void OnDamage(float damage);
}
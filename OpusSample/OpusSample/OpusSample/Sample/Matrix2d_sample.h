#pragma once
#include <cmath>





//2�����x�N�g���N���X
class Vector2 {
public:
	double x;
	double y;

	void setx(double x_);

	void sety(double y_);

	void set(const Vector2& source);

	//�R�s�[�R���X�g���N�^
	Vector2(const Vector2& source);

	//�R���X�g���N�^
	Vector2();

	//�R���X�g���N�^
	Vector2(double x, double y);
};

//3x3�s��N���X
class Matrix3x3 {
public:
	double a;			//11
	double b;			//12
	double c;			//13
	double d;			//21
	double e;			//22
	double f;			//23
	double g;			//31
	double h;			//32
	double i;			//33

	Matrix3x3();

	//�ړ�
	void Move(Vector2& source);

	//��]
	//�����Fangle �p�x[rad]
	void Rotate(double angle);

	//2�����x�N�g���Ƃ̏�Z
	Vector2 operator * (Vector2& source);
	//3x3�s��Ƃ̏�Z
	Matrix3x3 operator * (Matrix3x3& source);
};



inline void Vector2::setx(double x_) {
	this->x = x;
}

inline void Vector2::sety(double y_) {
	this->y = y;
}

inline void Vector2::set(const Vector2& source) {
	x = source.x;
	y = source.y;
}

//�R�s�[�R���X�g���N�^
inline Vector2::Vector2(const Vector2& source) {
	x = source.x;
	y = source.y;
}

//�R���X�g���N�^
inline Vector2::Vector2() {
	x = 0;
	y = 0;
}

//�R���X�g���N�^
inline  Vector2::Vector2(double x, double y) {
	this->x = x;
	this->y = y;
}




inline	Matrix3x3::Matrix3x3() {
	//������
	a = 1.0;
	b = 0;
	c = 0;
	d = 0;
	e = 1.0;
	f = 0;
	g = 0;
	h = 0;
	i = 1.0;
}

	//�ړ�
inline void Matrix3x3::Move(Vector2& source) {
	c = source.x;
	f = source.y;
}

	//��]
	//�����Fangle �p�x[rad]
inline	void Matrix3x3::Rotate(double angle) {
	a = cos(angle);
	b = -sin(angle);
	d = sin(angle);
	e = cos(angle);
}

	//2�����x�N�g���Ƃ̏�Z
inline	Vector2 Matrix3x3::operator * (Vector2& source) {

	Vector2 vector = Vector2(0, 0);		//���ʊi�[�p
	vector.x = a * source.x + b * source.y + c;
	vector.y = d * source.x + e * source.y + f;

	return vector;
}
	//3x3�s��Ƃ̏�Z
inline Matrix3x3 Matrix3x3::operator * (Matrix3x3& source) {
	Matrix3x3 result;
	result.a = this->a * source.a + this->b * source.d + this->c * source.g;	//11
	result.b = this->a * source.b + this->b * source.e + this->c * source.h;	//12
	result.c = this->a * source.c + this->b * source.f + this->c * source.i;	//13
	result.d = this->d * source.a + this->e * source.d + this->f * source.g;	//21
	result.e = this->d * source.b + this->e * source.e + this->f * source.h;	//22
	result.f = this->d * source.c + this->e * source.f + this->f * source.i;	//23
	result.g = this->g * source.a + this->h * source.d + this->i * source.g;	//31
	result.h = this->g * source.b + this->h * source.e + this->i * source.h;	//32
	result.i = this->g * source.c + this->h * source.f + this->i * source.i;	//33

	return result;
}








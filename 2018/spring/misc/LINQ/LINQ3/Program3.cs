using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp15 {
	class B {//сведения о товарах, содержащие поля «Артикул товара», «Категория», «Страна-производитель»;
		public string Country;
		public string Code;
		public string Category;
	}

	class C {//скидки для потребителей в различных магазинах, содержащие поля «Код потребителя», «Название магазина», «Скидка (в процентах)»;
		public double Discount;
		public string UserCode;
		public string ShopName;
	}

	class D {//цены товаров в различных магазинах, содержащие поля «Артикул товара», «Название магазина», «Цена (в рублях)»;
		public string Code;
		public double Price;
		public string ShopName;
	}

	class E {//сведения о покупках потребителей в различных магазинах, содержащие поля «Код потребителя», «Артикул товара», «Название магазина».
		public string UserCode;
		public string Code;
		public string ShopName;
	}

	class Program3 {
		static void Main(string[] args) {
			IEnumerable<B> b = new List<B>();//товары
			IEnumerable<C> c = new List<C>();//скидки
			IEnumerable<D> d = new List<D>();//цены
			IEnumerable<E> e = new List<E>();//покупки

			var ans = b
				.GroupBy(x => x.Category)//получаем группы всех товаров по категориям
				.Select(x => new {
					Category = x.Key,
					Groups = e
						.GroupBy(y => y.ShopName)//получаем группы покупок по магазинам
						.Select(y => new {shopName = y.Key, sum = y.Count() == 0 ? -1 : y.Sum(z/*z это конкр.покупка*/ => (c.Where(w/*w это скидка*/ => w.ShopName == y.Key && w.UserCode == z.UserCode).Count() > 0/*если скидка на покупку есть*/  ? (int)d.Where(pr/*pr это цена*/ => pr.ShopName == y.Key && pr.Code == z.Code).First().Price * c.Where(w/*w это скидка*/ => w.ShopName == y.Key && w.UserCode == z.UserCode).First().Discount : 0)) })
				});
		}
	}
}

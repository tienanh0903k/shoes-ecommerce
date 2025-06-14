import { Routes } from '@angular/router';
import { ShoppingCartComponent } from './User/Features/cart/shopping-cart/shopping-cart.component';
import { PaymentComponent } from './User/Features/payment/payment/payment.component';
import { AdminLayoutComponent } from './Layout/admin-layout/admin-layout.component';
import { UserLayoutComponent } from './Layout/user-layout/user-layout.component';
import { HomePageComponent } from './User/Pages/home-page/home-page.component';
import { ShopComponent } from './User/Pages/shop/shop.component';
import { ProductComponent } from './User/Pages/product/product.component';

import { ListCustomerComponent } from './admin/feature/customer/list-customer/list-customer.component';
import { ListOrderComponent } from './admin/feature/order/list-order/list-order.component';
import { AddproductComponent } from './admin/feature/product/addproduct/addproduct.component';
import { UpdateProductComponent } from './admin/feature/product/update-product/update-product.component';
import { AddBrandComponent } from './admin/feature/brand/add-brand/add-brand.component';

import { AddBlogComponent } from './admin/feature/blog/add-blog/add-blog.component';

// import { UpdateBlogComponent } from './admin/feature/blog/update-blog/update-blog.component';
// import { ListProductComponent } from './admin/feature/product/list-product/list-product.component';
// import { ListBlogComponent } from './admin/feature/blog/list-blog/list-blog.component';

import { PresonalInfoComponent } from './User/Features/presonal-info/presonal-info.component';
import { LoginComponent } from './User/Features/auth/login/login.component';
import { RegisterComponent } from './User/Features/auth/register/register.component';

// import { ListProductComponent } from './admin/feature/product/list-product/list-product.component';
// import { ListBlogComponent } from './admin/feature/blog/list-blog/list-blog.component';
import { IndexComponent } from './admin/Core/index/index.component';
import { ContactComponent } from './admin/Core/contact/contact.component';
import { UpdateBlogComponent } from './admin/feature/blog/update-blog/update-blog.component';
import { BlogListComponent } from './User/Core/Component/blog-list/blog-list.component';
import { BlogDetailComponent } from './User/Features/Blog/blog-detail/blog-detail.component';
import { BlogPageComponent } from './User/Features/Blog/blog-page/blog-page.component';

import { ListProductComponent } from './admin/feature/product/list-product/list-product.component';

import { ListBlogComponent } from './admin/feature/blog/list-blog/list-blog.component';
import { UpdateBrandsComponent } from './admin/feature/brand/update-brands/update-brands.component';
import { AddSizeComponent } from './admin/feature/size/add-size/add-size.component';
import { UpdateSizeComponent } from './admin/feature/size/update-size/update-size.component';

import { ListBrandComponent } from './admin/feature/brand/list-brand/list-brand.component';
import { ChangePasswordComponent } from './User/Features/change-password/change-password.component';
import { TrackingOrderComponent } from './User/Features/tracking-order/tracking-order.component';
import { authGuard } from './User/Features/auth/guard/auth.guard';

export const routes: Routes = [
  {
    path: 'admin',
    component: AdminLayoutComponent,
    canActivate: [authGuard],
    data: { roles: ['Admin'] },
    children: [
      {
        path: 'index',
        component: IndexComponent,
      },
      {
        path: 'brand',
        component: ListBrandComponent,
      },
      {
        path: 'contact',
        component: ContactComponent,
      },
      {
        path: 'customer',
        component: ListCustomerComponent,
      },
      {
        path: 'order',
        component: ListOrderComponent,
      },
      {
        path: 'product',
        component: ListProductComponent,
      },

      {
        path: 'blog',
        component: ListBlogComponent,
      },
      {
        path: 'product/add',
        component: AddproductComponent,
      },
      {
        path: 'product/update/:id',
        component: UpdateProductComponent,
      },
      {
        path: 'brand/add',
        component: AddBrandComponent,
      },
      {
        path: 'brand/update:id',
        component: UpdateBrandsComponent,
      },
      {
        path: 'blog/add',
        component: AddBlogComponent,
      },
      {
        path: 'blog/update/:id',

        component: UpdateBlogComponent,
      },
      {
        path: 'size/add',
        component: AddSizeComponent,
      },
      {
        path: 'size/update:id',
        component: UpdateSizeComponent,
      },
    ],
  },
  {
    path: '',
    component: UserLayoutComponent,
    children: [
      {
        path: 'register',
        component: RegisterComponent,
      },
      {
        path: 'login',
        component: LoginComponent,
      },
      {
        path: '',
        component: HomePageComponent,
      },

      {
        path: 'shop',
        component: ShopComponent,
      },

      {
        path: 'product/:id',
        component: ProductComponent,
      },

      {
        path: 'cart',
        component: ShoppingCartComponent,
        canActivate: [authGuard],
        data: { roles: ['User'] },
      },

      {
        path: 'checkout',
        component: PaymentComponent,
        canActivate: [authGuard],
        data: { roles: ['User'] },
      },

      {
        path: 'user/:id',
        component: PresonalInfoComponent,
        canActivate: [authGuard],
        data: { roles: ['User'] },
      },

      {
        path: 'user/order',
        component: TrackingOrderComponent,
      },

      {
        path: 'blog',
        component: BlogPageComponent,
      },

      {
        path: 'blog/:id',
        component: BlogDetailComponent,
      },
    ],
  },
];

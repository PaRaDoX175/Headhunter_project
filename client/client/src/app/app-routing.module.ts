import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { VacancyDetailsComponent } from './vacancy-details/vacancy-details.component';
import { AboutCompanyComponent } from './about-company/about-company.component';
import { VacancyListComponent } from './vacancy-list/vacancy-list.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ProfileComponent } from './profile/profile.component';
import { FavoritesComponent } from './favorites/favorites.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'catalog/:id', component: VacancyDetailsComponent },
  { path: 'company/:id', component: AboutCompanyComponent },
  { path: 'catalog', component: VacancyListComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'profile', component: ProfileComponent },
  { path: 'favorites', component: FavoritesComponent },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { scrollPositionRestoration: 'top' })],
  exports: [RouterModule],
})
export class AppRoutingModule {}

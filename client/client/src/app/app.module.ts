import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainComponent } from './main/main.component';
import { HttpClientModule } from '@angular/common/http';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { HomeComponent } from './home/home.component';
import { CatalogComponent } from './catalog/catalog.component';
import { QuickVacComponent } from './quick-vac/quick-vac.component';
import { PagerComponent } from './pager/pager.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { VacancyDetailsComponent } from './vacancy-details/vacancy-details.component';
import { CardComponent } from './card/card.component';
import { FooterComponent } from './footer/footer.component';
import { AboutCompanyComponent } from './about-company/about-company.component';
import { VacancyListComponent } from './vacancy-list/vacancy-list.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ProfileComponent } from './profile/profile.component';
import { FavoritesComponent } from './favorites/favorites.component';

@NgModule({
  declarations: [
    AppComponent,
    MainComponent,
    NavBarComponent,
    HomeComponent,
    CatalogComponent,
    QuickVacComponent,
    PagerComponent,
    VacancyDetailsComponent,
    CardComponent,
    FooterComponent,
    AboutCompanyComponent,
    VacancyListComponent,
    LoginComponent,
    RegisterComponent,
    ProfileComponent,
    FavoritesComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    PaginationModule.forRoot(),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}

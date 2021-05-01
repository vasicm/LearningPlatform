import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeModule } from './pages/home/home.module';
import { HomePageComponent } from './pages/home/home-page/home-page.component';
import { DictionaryModule } from './pages/dictionary/dictionary.module';
import { DictionaryPageComponent } from './pages/dictionary/dictionary-page/dictionary-page.component';
import { ComponentsModule } from './components/components.module';
import { TextModule } from './pages/text/text.module';
import { TextPageComponent } from './pages/text/text-page/text-page.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomePageComponent, pathMatch: 'full' },
      { path: 'dictionary', component: DictionaryPageComponent },
      { path: 'text', component: TextPageComponent },
    ]),
    HomeModule,
    DictionaryModule,
    ComponentsModule,
    TextModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { environment } from '../environments/environment';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AuthModule } from './features/auth/auth.module';
import { SharedModule } from './shared/modules/shared.module';


@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule, AppRoutingModule, HttpClientModule, FormsModule, 
    AuthModule, SharedModule,
  ],
  providers: [
    provideClientHydration(),
    { provide: 'apiUrl', useValue: environment.apiUrl }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

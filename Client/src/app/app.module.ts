import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { environment } from '../environments/environment';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AuthModule } from './features/auth/auth.module';
import { SharedModule } from './shared/modules/shared.module';
import { SharedModule } from './shared/modules/shared/shared.module';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';


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
  imports: [BrowserModule, AppRoutingModule, SharedModule],
  providers: [provideClientHydration(), provideAnimationsAsync()],
  bootstrap: [AppComponent]
})
export class AppModule { }

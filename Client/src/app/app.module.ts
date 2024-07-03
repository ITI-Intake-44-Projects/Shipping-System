import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { environment } from '../environments/environment';
import { HttpClientModule, provideHttpClient, withFetch } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthModule } from './features/auth/auth.module';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { SharedModule } from './shared/modules/shared.module';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';



@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule, AppRoutingModule, HttpClientModule,
    FormsModule, ReactiveFormsModule, AuthModule, SharedModule
  ],
  providers: [
    provideClientHydration(), provideHttpClient(withFetch()),
    { provide: 'apiUrl', useValue: environment.apiUrl },
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

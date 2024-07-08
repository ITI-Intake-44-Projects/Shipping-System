import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { environment } from '../environments/environment';
import { HttpClientModule, provideHttpClient, withFetch } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from './shared/modules/shared/shared.module';
import { SidebarComponent } from './shared/sidebar/sidebar.component';

@NgModule({
  declarations: [AppComponent, SidebarComponent],
  imports: [
    BrowserModule, AppRoutingModule, HttpClientModule,
    FormsModule, ReactiveFormsModule, SharedModule,
  ],
  providers: [
    provideClientHydration(), provideHttpClient(withFetch()),
    { provide: 'apiUrl', useValue: environment.apiUrl },
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

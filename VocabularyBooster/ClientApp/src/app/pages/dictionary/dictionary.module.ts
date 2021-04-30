import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DictionaryPageComponent } from './dictionary-page/dictionary-page.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ComponentsModule } from 'src/app/components/components.module';



@NgModule({
  declarations: [DictionaryPageComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ComponentsModule
  ],
  exports: [ComponentsModule]
})
export class DictionaryModule { }

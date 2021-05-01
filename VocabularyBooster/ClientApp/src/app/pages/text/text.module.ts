import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TextPageComponent } from './text-page/text-page.component';
import { ComponentsModule } from 'src/app/components/components.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [TextPageComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ComponentsModule
  ]
})
export class TextModule { }

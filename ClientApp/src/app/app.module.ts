import { TableEntityService } from './services/table-entity.service';
import { RelationshipsComponent } from './relationships/relationships.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ComponentFactoryResolver } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { TableComponent } from './table/table.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatButtonModule } from '@angular/material/button';
import { ColumnComponent } from './column/column.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatNativeDateModule } from '@angular/material/core';
import { DataFormComponent } from './data-form/data-form.component';
import { DatePipe } from '@angular/common';
import { TableDataFormComponent } from './table-data-form/table-data-form.component';
import { EntityComponent } from './entity/entity.component';

@NgModule({
  declarations: [
    AppComponent,
    TableComponent,
    ColumnComponent,
    DataFormComponent,
    TableDataFormComponent,
    EntityComponent,
    RelationshipsComponent

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    MatExpansionModule,
    MatCheckboxModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatButtonModule,
    FormsModule,
    ReactiveFormsModule
    ],
  providers: [DatePipe, TableEntityService],
  bootstrap: [AppComponent],
  entryComponents: [
    ColumnComponent,
    TableComponent,
    EntityComponent
  ]
})
export class AppModule { }

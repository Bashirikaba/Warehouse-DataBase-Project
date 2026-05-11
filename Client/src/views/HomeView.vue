<script setup lang="ts"></script>
<template>
  <Card>
    <template #content>
      <Fieldset legend="Сущности">
        <template #default>
          <div style="display: flex; gap: 10px">
            <Button label="Должности" as="router-link" :to="{ name: 'Positions' }"></Button>
            <Button label="Накладные" as="router-link" :to="{ name: 'Invoices' }"></Button>
          </div>
        </template>
      </Fieldset>
      <Fieldset legend="Отчёты">
        <Button label="отчет1"></Button>
      </Fieldset>
    </template>
  </Card>
</template>

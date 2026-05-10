<script setup lang="ts"></script>
<template>
  <Card>
    <template #content>
      <Fieldset legend="Сущности">
        <Button label="Должности" as="router-link" :to="{ name: 'Positions' }"></Button>
        <Button label="Накладные" as="router-link" :to="{ name: 'Invoices' }"></Button>
      </Fieldset>
      <Fieldset legend="Отчёты">
        <Button label="отчет1"></Button>
      </Fieldset>
    </template>
  </Card>
</template>

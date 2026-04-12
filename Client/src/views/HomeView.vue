<script setup lang="ts"></script>
<template>
  <Card>
    <template #content>
      <Fieldset legend="Сущности">
        <Button label="Должности" as="router-link" :to="{ name: 'Positions' }"></Button>
      </Fieldset>
      <Fieldset legend="Отчёты">
        <Button label="отчет1"></Button>
      </Fieldset>
    </template>
  </Card>
</template>
